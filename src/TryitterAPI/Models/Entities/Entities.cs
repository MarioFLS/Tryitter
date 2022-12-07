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

        public struct UpdateStudent
        {
            [MinLength(3, ErrorMessage = "O nome deve ter no minimo 3 caracteres")]
            public string? Name { get; set; }

            [MinLength(5, ErrorMessage = "A senha deve ter deve ter no minimo 3 caracteres")]
            public string? Password { get; set; }

            public UpdateStudent(string name, string password)
            {
                Name = name;
                Password = password;
            }
        }

        public struct UpdatePost
        {

            [MinLength(5, ErrorMessage = "O titulo deve ter no mínimo 5 caracteres")]
            public string? Title { get; set; } = "";

            [MinLength(5, ErrorMessage = "O texto deve ter no mínimo 5 caracteres")]
            [MaxLength(300, ErrorMessage = "O texto pode ter no máximo 300 caracteres")]
            public string? Text { get; set; } = "";

            public UpdatePost(string? title, string? text)
            {
                Title = title;
                Text = text;
            }
        }
    }
}
