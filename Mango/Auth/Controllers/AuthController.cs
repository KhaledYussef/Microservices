using Auth.Models;
using Auth.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _authService.Register(model);
            if (result == null)
            {
                return Ok(ResponseResult.Success("User registered successfully"));
            }
            else
            {
                return BadRequest(ResponseResult.Error(result));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO model)
        {
            var result = await _authService.Login(model);
            if (result == null)
            {
                return BadRequest(ResponseResult.Error("Wrong email or password"));
            }
            else
            {
                return Ok(ResponseResult.Success(result));
            }
        }
    }
}
