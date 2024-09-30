using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Web.Models;
using Web.Services;
using Web.Util;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService auth,
            ITokenService tokenService)
        {
            _auth = auth;
            _tokenService = tokenService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _auth.Login(loginDTO);
            if (result.IsSuccess)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(result.Data?.ToString() ?? "");
                await SignInAsync(loginResponse!);
                _tokenService.SetToken(loginResponse!.Token);
                TempData["success"] = "Login successful";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = result.Message;
                ModelState.AddModelError("CustomError", result.Message ?? "");
            }
            return View();
        }



        public IActionResult Register()
        {
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Value = Shared.RoleAdmin, Text = Shared.RoleAdmin },
                new SelectListItem { Value = Shared.RoleCustomer, Text = Shared.RoleCustomer }
            };
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _auth.Register(registerDTO);
            if (result.IsSuccess)
            {
                TempData["success"] = "User registered successfully";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["error"] = result.Message;
            }

            var roles = new List<SelectListItem>
            {
                new SelectListItem { Value = Shared.RoleAdmin, Text = Shared.RoleAdmin },
                new SelectListItem { Value = Shared.RoleCustomer, Text = Shared.RoleCustomer }
            };
            ViewBag.Roles = roles;
            return View();
        }


        private async Task SignInAsync(LoginResponseDTO model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(model.Token);
            var claims = new List<Claim>();
            foreach (var item in jsonToken.Claims)
            {
                claims.Add(new Claim(item.Type, item.Value));
            }
            claims.Add(new Claim(ClaimTypes.Name, jsonToken.Claims.FirstOrDefault(a => a.Type == "unique_name")?.Value));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrinciple = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrinciple);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _tokenService.RemoveToken();
            return RedirectToAction("Login");
        }
    }
}
