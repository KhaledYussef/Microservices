namespace Authentication.Services
{
    public interface IJwtTokenService
    {
        string? GenerateToken(string username, string password);
    }
}