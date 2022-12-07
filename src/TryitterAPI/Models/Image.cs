using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TryitterAPI.Models
{
    public class Image
    {
        public int Id { get; set; }

        [DefaultValue(null)]
        [DataType(DataType.Url)]
        public string? Link { get; set; } = null;

        public int PostId { get; set; }
    }
}
