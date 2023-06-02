using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecretSharing.Core.Entities.Identity;
using SecretSharing.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecretSharing.Application.CustomServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _Key;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }
        public string GenerateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, appUser.Email)
            };
            var credential = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha256);
            var TokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = appUser.Email,
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credential,
                Issuer = _configuration["Token:Issuer"],
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(TokenDesc);
            return tokenhandler.WriteToken(token);
        }
    }
}
