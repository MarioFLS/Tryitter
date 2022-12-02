using System.ComponentModel.DataAnnotations;

namespace TryitterAPI.Models
{
    public class Image
    {
        public int Id { get; set; }

        [DataType(DataType.Url)]
        public string? Link { get; set; }

        [Range(1, 1, ErrorMessage = "Digite um id válido")]
        public int PostId { get; set; }

    }
}
