using Authentication.Models;
using Authentication.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IJwtTokenService jwtTokenService) : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel request)
        {
            var token = _jwtTokenService.GenerateToken(request.Username, request.Password);
            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}
