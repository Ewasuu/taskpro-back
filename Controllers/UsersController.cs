using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskPro_back.Entities;
using TaskPro_back.Helpers;
using TaskPro_back.IRepository;

namespace TaskPro_back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _repository;
        public UsersController(IUserRepository repository) {
            _repository = repository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserDTO user, Guid id)
        {
            Guid tokenId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");

            if(!tokenId.Equals(id))
                return Unauthorized();

            ResponseDTO<UserDTO> response = await _repository.Update(user, id);

            if(response.Succes)
                return Ok(response);
            else 
                return NotFound(response);
        }

        [HttpGet("search/{key}")]
        public async Task<IActionResult> Search(string key)
        {
            ResponseDTO<IEnumerable<UserDTO>> response = await _repository.Get(key);

            if(response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid tokenId = JWTHelper.ValidateToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? "");

            if (!tokenId.Equals(id))
                return Unauthorized();

            ResponseDTO<bool> response = await _repository.Delete(id);

            if(response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
