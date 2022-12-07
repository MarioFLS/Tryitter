﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryitterAPI.Models;
using TryitterAPI.Repository;

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
            var studentId = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            Post newPost = new() { StudentId = studentId, Images = post.Images, Text = post.Text, Title = post.Title };

            _twitterRepository.AddPost(newPost);

            return CreatedAtAction(nameof(CreatePost), new { id = studentId }, newPost);

        }

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
    }
}
