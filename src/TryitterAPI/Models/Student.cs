using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryitterAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public List<Post> Posts { get; set; } = default!;

    }
}
