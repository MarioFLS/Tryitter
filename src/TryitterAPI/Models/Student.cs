using System.ComponentModel.DataAnnotations;

namespace TryitterAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3, ErrorMessage = "O nome deve ter no minimo 3 caracteres")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "O email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(5, ErrorMessage = "A senha deve ter deve ter no minimo 3 caracteres")]
        public string Password { get; set; } = default!;
        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
