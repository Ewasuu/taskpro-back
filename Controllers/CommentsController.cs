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
        public CommentsController(ICommentRepository repository) {
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
    }
}
