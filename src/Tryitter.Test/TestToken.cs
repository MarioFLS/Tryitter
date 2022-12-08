using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TryitterAPI.Models;
using TryitterAPI.Services.Auth;

namespace Tryitter.Test
{
    public class TestToken
    {
        [Theory(DisplayName = "Testando se o token não é Nulo")]
        [InlineData("Mario", "email@test.com", "12345798")]
        [InlineData("Fernando", "emailDeverasTEstado@test.com", "12345698")]
        public void TestTokenGeneratorSuccess(string name, string email, string password)
        {
            Student student = new() { Name = name, Email = email, Id = 1, Password = password };
            TokenGenerator token = new();
            string response = token.Generate(student);
            response.Should().NotBeNull();
        }

        [Theory(DisplayName = "Teste para TokenGenerator em que token JWT possui as duas informações: ID e Email")]
        [InlineData("Mario", "email@test.com", "12345798")]
        [InlineData("Fernando", "emailDeverasTEstado@test.com", "12345698")]
        public void TestTokenGeneratorKeysSuccess(string name, string email, string password)
        {
            Student student = new() { Name = name, Email = email, Id = 1, Password = password };
            TokenGenerator tokenClass = new();
            string token = tokenClass.Generate(student);
            string[] response = token.Split(".");
            response.Length.Should().Be(3);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("2d74025e7bcf058897d8daaa99ae99b5");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var studentId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            var studentEmail = jwtToken.Claims.First(x => x.Type == "email").Value;
            studentId.Should().Be(1);
            studentEmail.Should().Be(email);
        }
    }
}