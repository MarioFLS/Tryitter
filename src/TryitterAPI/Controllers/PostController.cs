using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryitterAPI.Models;
using TryitterAPI.Repository;
using TryitterAPI.Services.Exception;

namespace TryitterAPI.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostController : ControllerBase
    {
        private readonly ITryitterRepository _twitterRepository;

        public PostController(ITryitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreatePost([FromBody] Post post)
        {
            try
            {


                string? token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (token == null)
                {
                    throw new InvalidTokenException("Token Inválido");
                }

                var studentId = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
                Post newPost = new() { StudentId = studentId, Images = post.Images, Text = post.Text, Title = post.Title };

                _twitterRepository.AddPost(newPost, token);
                
                return CreatedAtAction(nameof(CreatePost), new { id = studentId }, newPost);

            }
            catch (InvalidTokenException e)
            {

                return Unauthorized(new { message = e.Message });
            }

        }
    }
}
