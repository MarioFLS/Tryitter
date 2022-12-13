using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryitterAPI.Models;
using TryitterAPI.Repository;
using static TryitterAPI.Models.Entities.Entities;

namespace TryitterAPI.Controllers
{
    /// text  
    [ApiController]
    [Route("post")]
    public class PostController : ControllerBase
    {
        private readonly ITryitterRepository _twitterRepository;
        /// text  
        public PostController(ITryitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        /// <summary>
        /// Cria um novo post
        /// </summary>
        /// <returns>Retorna o novo Post Criado</returns>
        [HttpPost]
        [Authorize]
        public IActionResult CreatePost([FromBody] Post post)
        {
            var studentId = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            Post newPost = new() { StudentId = studentId, Images = post.Images, Text = post.Text, Title = post.Title };

            _twitterRepository.AddPost(newPost);

            return CreatedAtAction(nameof(CreatePost), new { id = studentId }, newPost);

        }

        /// <summary>
        /// Remove um post pelo ID
        /// </summary>
        /// <param name="id">Id do Departamento</param>
        /// <returns>Não tem retorno</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult RemovePost(int id)
        {
            var studentId = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            var post = _twitterRepository.GetPost(id);
            if (post == null)
            {
                return NotFound(new { Message = "Post não encontrado" });
            }
            if (post.StudentId != studentId)
            {
                return Unauthorized(new { Message = "Você não é dono desse post" });
            }
            _twitterRepository.RemovePost(post);
            return Ok();
        }

        /// <summary>
        /// Editar um Post
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     {
        ///         "title": "title",
        ///         "text": "text",
        ///     }
        ///
        /// </remarks>

        /// <returns>Não possui retorno</returns>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult EditPost([FromBody] UpdatePost updatePost, int id)
        {

            int idStudent = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);

            if (updatePost.Title == null && updatePost.Text == null)
            {
                return BadRequest(new { message = "Insira os dados corretamente" });
            }
            var post = _twitterRepository.GetPost(id);


            if (post == null)
            {
                return NotFound(new { Message = "Post não encontrado" });
            }
            if (post.StudentId != idStudent)
            {
                return Unauthorized(new { Message = "Você não é dono desse post" });
            }

            _twitterRepository.EditPost(post, updatePost);
            return Ok();

        }

        /// <summary>
        /// Buscar todos os posts de um usuário
        /// </summary>
        /// <returns>Retorna um array com posts</returns>
        [HttpGet("{id}")]
        public IActionResult AllPosts(int id)
        {
            var posts = _twitterRepository.AllPosts(id);
            if (posts == null)
            {
                return NotFound(new { Message = "Usuário não encontrado" });
            }
            return Ok(posts);
        }

        /// <summary>
        /// Buscar o ultimo de um usuário
        /// </summary>
        /// <returns>Retorna uma postagem</returns>
        [HttpGet("{id}/last")]
        public IActionResult LastPost(int id)
        {
            var posts = _twitterRepository.LastPost(id);
            if (posts == null)
            {
                return NotFound(new { Message = "Usuário não encontrado ou não possui ultimo post" });
            }

            return Ok(posts);
        }
    }
}
