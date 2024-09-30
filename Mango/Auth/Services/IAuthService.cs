using Auth.Models;

namespace Auth.Services
{
    public interface IAuthService
    {
        Task<string?> Register(RegisterDTO registerDTO);
        Task<LoginResponseDTO?> Login(LoginDTO loginDTO);
    }
}
