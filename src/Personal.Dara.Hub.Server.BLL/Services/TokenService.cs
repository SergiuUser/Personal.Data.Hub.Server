using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Personal.Dara.Hub.Server.BLL.Services.Interfaces;
using Personal.Dara.Hub.Server.Models.Models.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Personal.Dara.Hub.Server.BLL.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserInfoToGenerateJwtToken user)
        {
            try
            {
                var privateKey = _configuration.GetValue<string>("Jwt:Key");
                var issuer = _configuration.GetValue<string>("Jwt:Issuer");
                var audience = _configuration.GetValue<string>("Jwt:Audience");

                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(privateKey);
                var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),  
                    SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = GenerateClaims(user),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = credentials,
                    Issuer = issuer,
                    Audience = audience,
                };

                var token = handler.CreateToken(tokenDescriptor);
                return handler.WriteToken(token);
            }
            catch {
                throw;
            }
        }

        private static ClaimsIdentity GenerateClaims(UserInfoToGenerateJwtToken user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            claims.AddClaim(new Claim(ClaimTypes.Role, user.Role.GetType().ToString()));

            return claims;
        }

    }
}
