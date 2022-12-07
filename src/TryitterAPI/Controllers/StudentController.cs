using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TryitterAPI.Models;
using TryitterAPI.Repository;
using static TryitterAPI.Models.Entities.Entities;

namespace TryitterAPI.Controllers
{

    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly ITryitterRepository _twitterRepository;

        public StudentController(ITryitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            string token = _twitterRepository.CreateStudent(student);
            if (token.IsNullOrEmpty())
            {
                return Problem("Já existe usuário com esse email", default, 400);
            }
            return StatusCode(201, new { token });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult StudentLogin([FromBody] Login login)
        {
            string token = _twitterRepository.StudentLogin(login);
            if (token.IsNullOrEmpty())
            {
                return Problem("Não existe Usuário com esse email", default, 400);
            }
            return StatusCode(201, new { token });

        }

        [HttpPut]
        [Authorize]
        public IActionResult EditStudent([FromBody] UpdateStudent updateStudent)
        {

            int id = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);

            if (updateStudent.Name == null && updateStudent.Password == null)
            {
                return BadRequest(new { message = "Insira os dados corretamente" });
            }
            var student = _twitterRepository.GetStudent(id);

            if (student == null)
            {
                return NotFound(new { message = "Estudante não encontrado" });
            }

            _twitterRepository.EditStudent(student, updateStudent);
            return Ok();

        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteStudent()
        {

            int id = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            var student = _twitterRepository.GetStudent(id);

            if (student == null)
            {
                return NotFound(new { message = "Estudante não encontrado" });
            }

            _twitterRepository.RemoveStudent(student);
            return Ok();

        }

        [HttpGet("post")]
        [Authorize]
        public IActionResult AllPosts()
        {
            int id = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            var posts = _twitterRepository.AllPosts(id);
            return Ok(posts);
        }

        [HttpGet("post/last")]
        [Authorize]
        public IActionResult LastPost()
        {
            int id = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            var posts = _twitterRepository.LastPost(id);
            return Ok(posts);
        }
    }
}
