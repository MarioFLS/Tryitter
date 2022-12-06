using TryitterAPI.Models;
using TryitterAPI.Services.Auth;
using TryitterAPI.Services.Exception;
using static TryitterAPI.Models.Entities.Entities;

namespace TryitterAPI.Repository
{
    public class TryitterRepository : ITryitterRepository
    {
        private readonly TryitterContext _context;
        private readonly TokenGenerator _tokenGenerator = new();

        public TryitterRepository(TryitterContext context)
        {
            _context = context;
        }

        public string CreateStudent(Student student)
        {
            Student? studentShearch = _context.Students.Where(e => e.Email == student.Email).FirstOrDefault();

            if (studentShearch != null)
            {
                return "";
            }
            _context.Students.Add(student);
            _context.SaveChanges();

            return _tokenGenerator.Generate(student);
        }

        public string StudentLogin(Login login)
        {
            Student? student = _context.Students.Where(e => e.Email == login.Email && e.Password == login.Password).FirstOrDefault();

            if (student == null)
            {
                return "";
            }

            return _tokenGenerator.Generate(student);
        }

        public void AddPost(Post post)
        {

            _context.Post.Add(post);
            _context.SaveChanges();
        }
    }
}
