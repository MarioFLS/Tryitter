namespace TryitterAPI.Models.Entities
{
    public class Entities
    {
        public struct TokenText
        {
            public string Token { get; set; }

            public TokenText(string token)
            {
                Token = token;
            }
        }

        public struct Login
        {
            public string Email { get; set; }
            public string Password { get; set; }

            public Login(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }
    }
}
