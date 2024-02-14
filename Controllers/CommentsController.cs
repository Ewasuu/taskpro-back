using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPro_back.Entities;
using TaskPro_back.IRepository;
using TaskPro_back.Models.MongoModels;

namespace TaskPro_back.Controllers
{
    [Authorize]
    [Controller]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        public CommentsController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Comment comment)
        {
            ResponseDTO<Comment> response = await _repository.Create(comment);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);

        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> Get(Guid taskId)
        {
            ResponseDTO<IEnumerable<Comment>> response = await _repository.Get(taskId);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> Update([FromBody] Comment comment, Guid commentId)
        {

            ResponseDTO<Comment> response = await _repository.Update(comment, commentId);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(Guid commentId)
        {
            ResponseDTO<Comment> response = await _repository.Delete(commentId);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
