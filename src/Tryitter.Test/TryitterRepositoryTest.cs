
using TryitterAPI.Models;
using TryitterAPI.Models.Entities;
using TryitterAPI.Repository;

namespace Tryitter.Test
{
    public class TryitterRepositoryTest
    {

        [Trait("Estudante", "1 - Endpoint para criar novo Estudante")]
        [Theory(DisplayName = "Criar um novo estudante e retorna um token")]
        [MemberData(nameof(TestCreateNewStudentData))]
        public void TestCreateNewStudent(TryitterContext context, string name, string email, string password)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Student student = new() { Name = name, Email = email, Password = password };
            var token = _tryitterRepository.CreateStudent(student);

            token.Should().NotBeNull();
            Student response = context.Students.Where(s => s.Email == email).FirstOrDefault()!;
            response.Email.Should().Be(email);

            var responseFail = _tryitterRepository.CreateStudent(student);
            responseFail.Should().BeNull();

        }
        public readonly static TheoryData<TryitterContext, string, string, string> TestCreateNewStudentData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestCreateNewStudent"),
                "Carlos",
                "emailDeverasTEstado@test.com",
                "12345698"
            },
        };

        [Trait("Estudante", "2 - Endpoint para logar como Estudante")]
        [Theory(DisplayName = "Gera um Token apartir do Login")]
        [MemberData(nameof(TestLoginStudentData))]
        public void TestLoginStudent(TryitterContext context, string email, string password, string fakeEmail)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Entities.Login login = new() { Email = email, Password = password };
            var token = _tryitterRepository.StudentLogin(login);

            token.Should().NotBeNull();

            Entities.Login fakeLogin = new() { Email = fakeEmail, Password = password };
            var response = _tryitterRepository.StudentLogin(fakeLogin);
            response.Should().BeNull();


        }
        public readonly static TheoryData<TryitterContext, string, string, string> TestLoginStudentData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestLoginStudent"),
                "emailFernando@teste.com",
                "123456789",
                "emailDeverasTEstado@test.com"
            },
        };

        [Trait("Post", "3 - Endpoint para criar uma postagem")]
        [Theory(DisplayName = "Cria um novo Post")]
        [MemberData(nameof(TestNewPostData))]
        public void TestNewPost(TryitterContext context, Post post)
        {
            TryitterRepository? _tryitterRepository = new(context);
            _tryitterRepository.AddPost(post);


            var response = context.Post.Where(p => p.Id == 5).FirstOrDefault()!;
            response.Should().BeEquivalentTo(post);


        }
        public readonly static TheoryData<TryitterContext, Post> TestNewPostData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestNewPost"),
                new Post {
                Id = 5,
                Title =  "Titulo Impressionante",
                Text = "Texto Impressionante",
                StudentId = 1,
                }
            },
        };

        [Trait("Post", "4 - Endpoint para buscar Estudante")]
        [Theory(DisplayName = "Busca Estudante")]
        [MemberData(nameof(TestGetStudentData))]
        public void TestGetStudent(TryitterContext context, int id)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Student student = _tryitterRepository.GetStudent(id)!;


            student.Id.Should().Be(id);
            student.Email.Should().Be("emailJorge@teste.com");


        }
        public readonly static TheoryData<TryitterContext, int> TestGetStudentData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestGetStudent"),
                3
            },
        };
    }
}
