using System.ComponentModel.DataAnnotations;

namespace TryitterAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;

        [MaxLength(300, ErrorMessage = "O texto pode ter no máximo 300 palavras")]
        public string Text { get; set; } = default!;

        [Range(1, 1, ErrorMessage = "Digite um id válido")]
        public int StudentId { get; set; }

        public List<Image>? Images { get; set; } = new List<Image>();
    }
}
