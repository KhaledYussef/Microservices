using Microsoft.IdentityModel.Tokens;

using System.Text;
using JwtExtensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Authentication.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly List<User> _users =
        [
            new User { Id = 1, Username = "admin", Role = "admin", Password = "admin", Scopes = ["writers.read"] },
            new User { Id = 2, Username = "user", Role = "user", Password = "user", Scopes = ["writers.read"] }
        ];

        public string? GenerateToken(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtension.SecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(30);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username ?? ""),
                new Claim("role", user.Role ?? ""),
                new Claim("scope", string.Join(" ", user.Scopes ?? new List<string>()))
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: JwtExtension.Issuer,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;

        }
    }
}
