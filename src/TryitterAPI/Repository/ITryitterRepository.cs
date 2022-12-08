using TryitterAPI.Models;
using TryitterAPI.Models.Entities;

namespace TryitterAPI.Repository
{
    public interface ITryitterRepository
    {
        string? CreateStudent(Student student);
        string? StudentLogin(Entities.Login login);
        void AddPost(Post post);

        Student? GetStudent(int id);
        Post? GetPost(int id);
        void EditStudent(Student student, Entities.UpdateStudent updateStudent);
        void RemoveStudent(Student student);

        void EditPost(Post post, Entities.UpdatePost updatePost);
        void RemovePost(Post post);

        List<Post>? AllPosts(int id);
        Post? LastPost(int id);
    }
}
