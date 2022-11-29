using System.ComponentModel.DataAnnotations;

namespace TryitterAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [MaxLength(300, ErrorMessage = "O texto pode ter no máximo 300 palavras")]
        public string Text { get; set; }
        public Student Student { get; set; }

        public Post()
        {
        }

        public Post(int id, string title, string text, Student student)
        {
            Id = id;
            Title = title;
            Text = text;
            Student = student;
        }
    }
}
