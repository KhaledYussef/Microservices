using Auth.Data;
using Auth.Models;

using MessageBus;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IMessageBus _messageBus;



        public AuthService(UserManager<ApplicationUser> userManager,
            AppDbContext db,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtOptions> jwtOptions,
            IMessageBus messageBus)
        {
            _userManager = userManager;
            _db = db;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions.Value;
            _messageBus = messageBus;
        }

        public async Task<LoginResponseDTO?> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    return null;
                }

                var checkPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

                if (!checkPassword)
                {
                    return null;
                }

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.PhoneNumber,
                };

                // generate token

                return new LoginResponseDTO
                {
                    User = userDTO,
                    Token = GenerateToken(user),
                };

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string?> Register(RegisterDTO registerDTO)
        {
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = registerDTO.Email,
                    Email = registerDTO.Email,
                    Name = registerDTO.Name,
                    PhoneNumber = registerDTO.Phone,

                };

                var result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (!result.Succeeded)
                {
                    return result.Errors.First().Description;
                }

                // add user to role
                registerDTO.Role ??= "CUSTOMER";
                var role = await _roleManager.FindByNameAsync(registerDTO.Role);
                if (role == null)
                {
                    // create the role
                    role = new IdentityRole(registerDTO.Role);
                    await _roleManager.CreateAsync(role);
                }

                await _userManager.AddToRoleAsync(user, role.Name ?? "CUSTOMER");

                // send message to message bus
                await _messageBus.Publish(user.Email, "registeruser");



                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private string GenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret!);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.Name!),
            };

            var userRoles = _userManager.GetRolesAsync(user).Result;
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(55555),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }





    }
}
