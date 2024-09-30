using Web.Models;
using Web.Util;

namespace Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpService _http;

        public AuthService(IHttpService httpService)
        {
            _http = httpService;
        }

        public async Task<ResponseResult> Login(LoginDTO loginDTO)
        {
            RequestDTO request = new RequestDTO
            {
                Url = Shared.AuthApi + "/api/auth/login",
                ApiType = ApiType.POST,
                Body = loginDTO
            };

            var result = await _http.SendAsync(request);
            return result;

        }

        public async Task<ResponseResult> Register(RegisterDTO registerDTO)
        {
            RequestDTO request = new RequestDTO
            {
                Url = Shared.AuthApi + "/api/auth/register",
                ApiType = ApiType.POST,
                Body = registerDTO
            };

            var result = await _http.SendAsync(request);
            return result;
        }
    }
}
