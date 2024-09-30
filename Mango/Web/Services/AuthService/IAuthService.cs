
using Web.Models;

namespace Web.Services
{
    public interface IAuthService
    {
        Task<ResponseResult> Login(LoginDTO loginDTO);
        Task<ResponseResult> Register(RegisterDTO registerDTO);
    }
}