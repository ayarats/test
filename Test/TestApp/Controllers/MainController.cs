using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using WebService.Interfaces;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly IService _service;
        private readonly IMapper _mapper;

        public MainController(IService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            var domainPost = await _service.GetPost(id);
            if (domainPost != null)
            {
                return Ok(_mapper.Map<PostModel>(domainPost));
            }
            return NotFound();
        }

        [HttpPost("post")]
        public async Task<IActionResult> AddPost(PostModel post)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var add = await _service.AddPost(_mapper.Map<Post>(post));
                    return Ok(add);
                }
                catch (ArgumentNullException ex)
                {
                    return BadRequest("value cannot be null.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Inner exception.");
                }
            }
            return BadRequest(post);
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddComment(CommentModel comment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var add = await _service.AddComment(_mapper.Map<Comment>(comment));
                    return Ok(add);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest("Value cannot be null.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Inner exception.");
                }
            }
            return BadRequest(comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
            {
                return BadRequest();
            }
            try
            {
                var delete = await _service.Delete(postId);
                return Ok(delete);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Value cannot be null.");
            }
            catch (Exception ex)
            {
                return BadRequest("Inner exception.");
            }
        }
    }
}