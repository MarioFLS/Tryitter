using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryitterAPI.Models;
using TryitterAPI.Repository;
using static TryitterAPI.Models.Entities.Entities;

namespace TryitterAPI.Controllers
{
    ///text
    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly ITryitterRepository _twitterRepository;
        ///text
        public StudentController(ITryitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        /// <summary>
        /// Cria um novo Estudante
        /// </summary>
        /// <returns>Retorna Um token com as informações do estudante</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            string? token = _twitterRepository.CreateStudent(student);
            if (token == null)
            {
                return Problem("Já existe usuário com esse email", default, 400);
            }
            return StatusCode(201, new { token });
        }

        /// <summary>
        /// Permite fazer Login como Estudante
        /// </summary>
        /// <returns>Retorna Um token com as informações do estudante</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult StudentLogin([FromBody] Login login)
        {
            string? token = _twitterRepository.StudentLogin(login);
            if (token == null)
            {
                return Problem("Não existe Usuário com esse email", default, 400);
            }
            return StatusCode(201, new { token });

        }

        /// <summary>
        /// Editar um Post
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     {
        ///         "name": "name",
        ///         "text": "text",
        ///     }
        ///
        /// </remarks>

        /// <returns>Não possui retorno</returns>
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

        /// <summary>
        /// Remove o Seu estudante
        /// </summary>
        /// <returns>Não tem retorno</returns>
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

        /// <summary>
        /// Buscar todos os posts do estudante
        /// </summary>
        /// <returns>Retorna um array com posts</returns>
        [HttpGet("post")]
        [Authorize]
        public IActionResult AllPosts()
        {
            int id = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            var posts = _twitterRepository.AllPosts(id);
            return Ok(posts);
        }

        /// <summary>
        /// Buscar a ultima postagem do estudante
        /// </summary>
        /// <returns>Retorna uma postagem</returns>
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
