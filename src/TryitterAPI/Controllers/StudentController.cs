using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TryitterAPI.Models;
using TryitterAPI.Models.Entities;
using TryitterAPI.Repository;

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
        public IActionResult StudentLogin([FromBody] Entities.Login login)
        {
            string token = _twitterRepository.StudentLogin(login);
            if (token.IsNullOrEmpty())
            {
                return Problem("Não existe Usuário com esse email", default, 400);
            }
            return StatusCode(201, new { token });

        }
    }
}
