using System.ComponentModel.DataAnnotations;
using TaskPro_back.Entities;
using TaskPro_back.Models;

namespace TaskPro_back.IRepository
{
    public interface IUserRepository
    {
        public Task<LoginResponseDTO<UserDTO>> Login(LoginDTO registerDTO);
        public Task<ResponseDTO<UserDTO>> Create(RegisterDTO registerDTO);
        public Task<ResponseDTO<IEnumerable<UserDTO>>> Get(string key);
        public Task<ResponseDTO<UserDTO>> Update(UserDTO userDTO, Guid id);
        public Task<ResponseDTO<bool>> Delete(Guid id);
    }
}
