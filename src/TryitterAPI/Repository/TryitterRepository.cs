using Microsoft.EntityFrameworkCore;
using TryitterAPI.Models;
using TryitterAPI.Services.Auth;
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

        public string? CreateStudent(Student student)
        {
            Student? studentShearch = _context.Students.Where(e => e.Email == student.Email).FirstOrDefault();

            if (studentShearch != null)
            {
                return null;
            }
            _context.Students.Add(student);
            _context.SaveChanges();

            return _tokenGenerator.Generate(student);
        }

        public string? StudentLogin(Login login)
        {
            Student? student = _context.Students.Where(e => e.Email == login.Email && e.Password == login.Password).FirstOrDefault();

            if (student == null)
            {
                return null;
            }

            return _tokenGenerator.Generate(student);
        }

        public Student? GetStudent(int id)
        {
            return _context.Students.Find(id);
        }

        public Post? GetPost(int id)
        {
            return _context.Post.Where(p => p.Id == id).FirstOrDefault();
        }

        public void AddPost(Post post)
        {

            _context.Post.Add(post);
            _context.SaveChanges();
        }

        public void EditStudent(Student student, UpdateStudent updateStudent)
        {
            student.Name = updateStudent.Name ?? student.Name;
            student.Password = updateStudent.Password ?? student.Password;
            _context.SaveChanges();
        }

        public void RemoveStudent(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public void EditPost(Post post, UpdatePost updatePost)
        {
            post.Text = updatePost.Text ?? post.Text;
            post.Title = updatePost.Title ?? post.Title;
            _context.SaveChanges();
        }

        public void RemovePost(Post post)
        {
            var images = _context.Post.Find(post.Id)!.Images?.RemoveAll(e => e.PostId == post.Id);
            _context.Post.Remove(post);

            _context.SaveChanges();
        }

        public List<Post>? AllPosts(int id)
        {
            var student = _context.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
            {
                return null;
            }
            return _context.Post.Include(p => p.Images).Where(p => p.StudentId == id).OrderByDescending(p => p.Id).ToList();

        }

        public Post? LastPost(int id)
        {
            var student = _context.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
            {
                return null;
            }
            var post = _context.Post.Include(p => p.Images).Where(p => p.StudentId == id).OrderBy(p => p.Id).ToList();
            if (post.Count == 0)
            {
                return post.FirstOrDefault();
            }
            return post.Last();
        }
    }
}
