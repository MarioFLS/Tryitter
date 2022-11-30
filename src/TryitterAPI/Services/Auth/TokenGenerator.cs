using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TryitterAPI.Models;

namespace TryitterAPI.Services.Auth
{
    public class TokenGenerator
    {
        public const string Secret = "2d74025e7bcf058897d8daaa99ae99b5";
        public string Generate(Student student)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(student),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
                Expires = DateTime.Now.AddDays(1)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity AddClaims(Student student)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Name, student.Name));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()));
            return claims;
        }
    }
}
