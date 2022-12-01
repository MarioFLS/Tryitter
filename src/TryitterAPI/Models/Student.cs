using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TryitterAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "O seu nome deve ter no minimo 3 caracteres")]
        public string Name { get; set; } = default!;

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email { get; set; } = default!;
        
        [MinLength(5, ErrorMessage = "A senha deve ter deve ter no minimo 3 caracteres")]
        public string Password { get; set; } = default!;
        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
