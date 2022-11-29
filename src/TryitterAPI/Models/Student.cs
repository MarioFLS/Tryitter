using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryitterAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }

        [Column("Post_Id")]
        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public ICollection<Post> Posts { get; set; }

        public Student(int id, string name, string email, string password, int postId, ICollection<Post> posts)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            PostId = postId;
            Posts = posts;
        }
    }
}
