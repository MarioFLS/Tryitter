using System.ComponentModel.DataAnnotations;

namespace TryitterAPI.Models
{
    public class Post
    {
        public int Id { get; set;  }

        [Required(ErrorMessage = "O titulo é obrigatório")]
        [MinLength(5, ErrorMessage = "O titulo deve ter no mínimo 5 caracteres")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "O texto é obrigatório")]
        [MinLength(5, ErrorMessage = "O texto deve ter no mínimo 5 caracteres")]
        [MaxLength(300, ErrorMessage = "O texto pode ter no máximo 300 caracteres")]
        public string Text { get; set; } = "";

        [Required(ErrorMessage = "O id do estudante é obrigatório")]
        [Range(1, 1, ErrorMessage = "Digite um id válido")]
        public int StudentId { get; set; }

        public List<Image>? Images { get; set; } = new List<Image>();
    }
}
