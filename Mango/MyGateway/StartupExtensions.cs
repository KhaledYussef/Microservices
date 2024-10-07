using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace MyGateway
{
    public static class StartupExtensions
    {
        public static void AddMyAuthentication(this WebApplicationBuilder builder)
        {

            var secret = builder.Configuration["JwtOptions:Secret"];
            var issuer = builder.Configuration["JwtOptions:Issuer"];
            var audience = builder.Configuration["JwtOptions:Audience"];
            var key = Encoding.ASCII.GetBytes(secret ?? "");
            builder.Services.AddAuthentication(a =>
            {
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                };
            });

        }
    }
}
