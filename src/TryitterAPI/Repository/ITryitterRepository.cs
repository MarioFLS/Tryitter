using TryitterAPI.Models;
using TryitterAPI.Models.Entities;

namespace TryitterAPI.Repository
{
    public interface ITryitterRepository
    {
        string CreateStudent(Student student);
        string StudentLogin(Entities.Login login);

        void AddPost(Post post);
    }
}
