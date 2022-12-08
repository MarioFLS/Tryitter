
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
    }
}
