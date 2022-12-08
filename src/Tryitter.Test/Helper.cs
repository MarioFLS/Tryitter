using Microsoft.EntityFrameworkCore;
using TryitterAPI.Models;
using TryitterAPI.Repository;

namespace Tryitter.Test
{
    public class Helper
    {
        public static TryitterContext GetContextInstanceForTests(string inMemoryDbName)
        {
            var contextOptins = new DbContextOptionsBuilder<TryitterContext>()
                .UseInMemoryDatabase(inMemoryDbName)
                .Options;

            var context = new TryitterContext(contextOptins);
            Console.WriteLine(context);
            
            context.Students.AddRange(GetStudentListForTests());
            context.SaveChanges();

            context.Post.AddRange(GetSPostListForTests());
            context.SaveChanges();


            return context;
        }

        public static List<Student> GetStudentListForTests() =>
        new() {
            new Student {
                Id = 1,
                Name =  "Mario",
                Email = "email@teste.com",
                Password = "123456789"
            },
            new Student {
                Id = 2,
                Name =  "Fernando",
                Email = "emailFernando@teste.com",
                Password = "123456789"
            },
            new Student {
                Id = 3,
                Name =  "Jorge",
                Email = "emailJorge@teste.com",
                Password = "123456789"
            },
        };

        public static List<Post> GetSPostListForTests() =>
        new() {
            new Post {
                Id = 1,
                Title =  "Titulo claramente bem pensado",
                Text = "Texto extremamente explicativo",
                StudentId = 1,
            },
            new Post {
                Id = 2,
                Title =  "Titulo claramente muito deveras bem pensado",
                Text = "Texto extremamente explicativo sobre a explicação",
                StudentId = 3,
            },
            new Post {
                Id = 3,
                Title =  "Titulo desesperado por criatividade",
                Text = "Texto extremamente (não) criativo",
                StudentId = 2,
            },
            new Post {
                Id = 4,
                Title =  "Titulo final",
                Text = "Ultima Postagem",
                StudentId = 2,
            },
        };
    }
}
