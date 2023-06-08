using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecretSharing.Core.Entities.Identity;
using SecretSharing.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecretSharing.Application.CustomServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _Key;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Get the secret key in appsetings.json
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }
        public string GenerateToken(AppUser appUser)
        {
            // Create new claims include user'data
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
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
            // Sign the data with signing algorithm
            var token = tokenhandler.CreateToken(TokenDesc);
            return tokenhandler.WriteToken(token);
        }
    }
}
