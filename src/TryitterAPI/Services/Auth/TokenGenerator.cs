using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TryitterAPI.Models;
using TryitterAPI.Repository;

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

            claims.AddClaim(new Claim("name", student.Name));
            claims.AddClaim(new Claim("email", student.Email));
            claims.AddClaim(new Claim("id", student.Id.ToString()));
            return claims;
        }

        // Validação de Token
        // https://jasonwatmore.com/post/2022/01/19/net-6-create-and-validate-jwt-tokens-use-custom-jwt-middleware
        public static Student? ValidateToken(string token, TryitterContext context)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var studentName = jwtToken.Claims.First(x => x.Type == "name").Value;
                var studentEmail = jwtToken.Claims.First(x => x.Type == "email").Value;
                var student = context.Students.Where(u => u.Name == studentName && u.Email == studentEmail).First();

                return student;
            }
            catch
            {
                return null;
            }
        }
    }

}
