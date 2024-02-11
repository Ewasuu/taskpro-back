using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskPro_back.Entities;
using TaskPro_back.Helpers;
using TaskPro_back.IRepository;
using TaskPro_back.Models;

namespace TaskPro_back.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly TaskProContext _context;

        public UserRepository(TaskProContext context)
        {
            _context = context;
        }


        public async Task<ResponseDTO<UserDTO>> Create(RegisterDTO registerDTO)
        {
            try
            {

                using var context = _context;


                if (await context.Users.AnyAsync(u => u.Email.Equals(registerDTO.Email)))
                {
                    throw new Exception("Este correo ya está en uso");
                }

                User user = new User
                {
                    id = Guid.NewGuid(),
                    Email = registerDTO.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                    UserName = registerDTO.UserName
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                UserDTO userDTO = new UserDTO
                {
                    Email = user.Email,
                    Id = user.id,
                    Name = user.UserName
                };

                return new ResponseDTO<UserDTO> {
                    Data = userDTO,
                    Succes = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<UserDTO>
                {
                    Data = null,
                    Succes = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<LoginResponseDTO<UserDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                User? user = await _context.Users.AsNoTracking()
                    .Where(u => u.Email.Equals(loginDTO.Email))
                    .FirstOrDefaultAsync();

                if(user != null)
                {
                    bool isVerified = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);

                    if (isVerified)
                    {
                        return new LoginResponseDTO<UserDTO>
                        {
                            Data = new UserDTO { Email = user.Email, Id = user.id, Name = user.UserName},
                            Succes = true,
                            Token = JWTHelper.GenerateJwtToken(user)
                        };
                    }
                    else
                    {
                        throw new Exception("La contraseña es incorrecta");
                    }
                }
                else
                {
                    throw new Exception("No existe un usuario con este correo");
                }
            }
            catch (Exception ex)
            {

                return new LoginResponseDTO<UserDTO>
                {
                    Data = null,
                    Succes = false,
                    ErrorMesage = ex.Message
                };
            }
        }
        public async Task<ResponseDTO<bool>> Delete(Guid id)
        {
            try
            {
                using var context = _context;

                User? user = await context.Users.FindAsync(id) ?? null;

                if (user != null)
                { 
                    context.Users.Remove(user); 
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("No existe este usuario");
                }


                return new ResponseDTO<bool>
                {
                    Data = true,
                    Succes = true
                };

            }
            catch (Exception ex)
            {

                return new ResponseDTO<bool>
                {
                    Data = false,
                    Succes = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<UserDTO>>> Get(string key)
        {
            try
            {
                using var context = _context;

                IEnumerable<UserDTO> users = await (from user in context.Users.AsNoTracking()
                                                    where user.UserName.Contains(key) || user.Email.Contains(key)
                                                    select new UserDTO
                                                    {
                                                        Email = user.Email,
                                                        Name = user.UserName,
                                                        Id = user.id
                                                    }).ToListAsync();

                return new ResponseDTO<IEnumerable<UserDTO>>
                {
                    Data = users,
                    Succes = true
                };
            }
            catch (Exception ex)
            {

                return new ResponseDTO<IEnumerable<UserDTO>>
                {
                    Data = null,
                    Succes = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<UserDTO>> Update(UserDTO userDTO, Guid id)
        {
            try
            {
                using var context = _context;

                User? user = await context.Users.Where( u => u.id.Equals(id))
                    .FirstOrDefaultAsync();

                if (user != null) {
                    user.UserName = userDTO.Name;

                    await context.SaveChangesAsync();

                    return new ResponseDTO<UserDTO>
                    {
                        Data = userDTO,
                        Succes = true,
                    };
                }
                else
                {
                    throw new Exception("No se encontró el usuario");
                }
            }
            catch (Exception ex)
            {

                return new ResponseDTO<UserDTO>
                {
                    Data = null,
                    Succes = false,
                    ErrorMesage = ex.Message
                };
            };
        }


    }
}
