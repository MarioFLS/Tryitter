using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TryitterAPI.Models.Auth
{
    public class TokenGenerator
    {
        public const string Secret = "2d74025e7bcf058897d8daaa99ae99b5";
        public string Generate()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
                Expires = DateTime.Now.AddDays(1)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
