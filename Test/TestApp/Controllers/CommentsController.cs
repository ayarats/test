using System;
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
    public class CommentsController : ControllerBase
    {

        private readonly ICommentsService _service;
        private readonly IMapper _mapper;

        public CommentsController(ICommentsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Please specify comment id.");
            }
            var comment = await _service.GetComment(id);
            return Ok(comment);
        }

        [HttpGet("{number}/{page}")]
        public IActionResult GetComments(int number, int page)
        {
            if (number == 0)
            {
                return BadRequest("Plase specify comments number.");
            }
            var comments = _service.GetComments(number, page);
            return Ok(comments);
        }

        [HttpGet]
        public IActionResult GetAllComments()
        {
            var comments = _service.GetAllComments();
            return Ok(comments);
        }
    }
}
