using System.ComponentModel.DataAnnotations;

namespace TryitterAPI.Models.Entities
{
    public class Entities
    {

        public struct Login
        {
            [DataType(DataType.EmailAddress)]
            [EmailAddress(ErrorMessage = "Digite um email válido")]
            public string Email { get; set; }
            [MinLength(5, ErrorMessage = "A senha deve ter deve ter no minimo 3 caracteres")]
            public string Password { get; set; }

            public Login(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }
    }
}
