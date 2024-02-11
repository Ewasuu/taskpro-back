using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPro_back.Entities;
using TaskPro_back.Helpers;
using TaskPro_back.IRepository;

namespace TaskPro_back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string filter = "all")
        {
            Guid userId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");


            ResponseDTO<IEnumerable<UserTasksDTO>> response = await _repository.Get(filter, userId);

            if(response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
            
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Guid userId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");

            ResponseDTO<Models.Task> response = await _repository.GetByID(id, userId);

            if (response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskDTO task)
        {
            Guid userId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");

            ResponseDTO<Models.Task> response = await _repository.Create(task, userId);

            if (response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] TaskDTO task, Guid id)
        {
            Guid userId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");

            ResponseDTO<Models.Task> response = await _repository.Update(task, id, userId);

            if (response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid userId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");

            ResponseDTO<Models.Task> response = await _repository.Delete(id, userId);

            if (response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("add_user/{id}")]
        public async Task<IActionResult> RevokeUser([FromBody] AddUserInTaskDTO addUserInTaskDTO, Guid id)
        {
            Guid ownerId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");

            ResponseDTO<bool> response = await _repository.AddUser(addUserInTaskDTO, id, ownerId);

            if (response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("revoke_user/{id}")]
        public async Task<IActionResult> RemoveUser([FromBody] AddUserInTaskDTO addUserInTaskDTO, Guid id)
        {
            Guid ownerId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");

            ResponseDTO<bool> response = await _repository.RemoveUser(addUserInTaskDTO, id, ownerId);

            if (response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
