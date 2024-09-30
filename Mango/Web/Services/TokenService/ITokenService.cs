namespace Web.Services
{
    public interface ITokenService
    {
        string? GetToken();
        void RemoveToken();
        void SetToken(string token);
    }
}