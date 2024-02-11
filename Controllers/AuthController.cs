using Microsoft.AspNetCore.Mvc;
using TaskPro_back.Entities;
using TaskPro_back.IRepository;

namespace TaskPro_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {

            LoginResponseDTO<UserDTO> response = await _repository.Login(user);

            if (response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {

            ResponseDTO<UserDTO> response = await _repository.Create(user);

            if (response.Succes)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
