﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskPro_back.Entities;
using TaskPro_back.IRepository;
using TaskPro_back.Models;

namespace TaskPro_back.Repository
{
    public class TaskRepository : ITaskRepository
    {

        private readonly TaskProContext _context;
        public TaskRepository(TaskProContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<Models.Task>> Create(TaskDTO taskDto, Guid userId)
        {
            try
            {
                using var context = _context;

                Models.Task task = new Models.Task
                {
                    id = Guid.NewGuid(),
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                UserInTask userInTask = new UserInTask
                {
                    Role = Enums.Roles.OWNER,
                    TaskId = task.id,
                    UserId = userId,
                };

                await context.Tasks.AddAsync(task);
                await context.UsersInTasks.AddAsync(userInTask);
                await context.SaveChangesAsync();

                return new ResponseDTO<Models.Task>
                {
                    Data = task,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                return new ResponseDTO<Models.Task>
                {
                    Data = null,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<Models.Task>> Update(TaskDTO taskDTO, Guid id, Guid userId)
        {
            try
            {
                using var context = _context;

                if (await _context.UsersInTasks.AnyAsync(d => d.TaskId.Equals(id) && d.UserId.Equals(userId) && (d.Role.Equals(Enums.Roles.WRITE_READ) || d.Role.Equals(Enums.Roles.OWNER))))
                {
                    Models.Task? task = await context.Tasks.FindAsync(id);

                    if (task != null)
                    {
                        task.Title = taskDTO.Title;
                        task.Description = taskDTO.Description;
                        task.IsCompleted = taskDTO.IsCompleted;
                        task.UpdatedAt = DateTime.Now;

                        await context.SaveChangesAsync();

                        return new ResponseDTO<Models.Task>
                        {
                            Data = task,
                            Success = true
                        };
                    }
                    else
                    {
                        throw new Exception("No se encuentra esta tarea");
                    }
                }
                else
                {
                    throw new Exception("No tiene permisos para modificar esta tarea");
                }
            }
            catch (Exception ex)
            {

                return new ResponseDTO<Models.Task>
                {
                    Data = null,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<Models.Task>> Delete(Guid id, Guid userId)
        {
            try
            {
                using var context = _context;

                if (await _context.Tasks.AnyAsync(t => t.id.Equals(id)))
                {

                    UserInTask? userInTask = await (from tasks in _context.Tasks.AsNoTracking()
                                                    join user_in_task in _context.UsersInTasks.AsNoTracking()
                                                    on tasks.id equals user_in_task.TaskId
                                                    where user_in_task.UserId.Equals(userId)
                                                    select user_in_task).FirstOrDefaultAsync();

                    if (userInTask != null)
                    {
                        if (userInTask.Role.Equals(Enums.Roles.OWNER))
                        {
                            await context.UsersInTasks.Where(d => d.TaskId.Equals(id)).ExecuteDeleteAsync();
                            await context.Tasks.Where(d => d.id.Equals(id)).ExecuteDeleteAsync();

                            return new ResponseDTO<Models.Task>
                            {
                                Data = null,
                                Success = true
                            };

                        }
                        else
                        {
                            throw new Exception("No puedes eliminar esta tarea porque no eres el propietario");
                        }
                    }
                    else
                    {
                        throw new Exception("La tarea no existe o no participas en ella.");
                    }
                }
                else
                {
                    throw new Exception("La tarea no existe");
                }

            }
            catch (Exception ex)
            {

                return new ResponseDTO<Models.Task>
                {
                    Data = null,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<UserTasksDTO>>> Get(Guid userId)
        {
            try
            {
                using var context = _context;

                IEnumerable<UserTasksDTO> taskList = await (from tasks in _context.Tasks.AsNoTracking()
                                                            join user_in_task in _context.UsersInTasks.AsNoTracking()
                                                            on tasks.id equals user_in_task.TaskId
                                                            where user_in_task.UserId.Equals(userId)
                                                            select new UserTasksDTO
                                                            {
                                                                id = tasks.id,
                                                                IsCompleted = tasks.IsCompleted,
                                                                Role = user_in_task.Role,
                                                                Title = tasks.Title,
                                                                Description = tasks.Description,
                                                                CreatedAt = tasks.CreatedAt,
                                                                UpdatedAt = tasks.UpdatedAt,
                                                            }).ToListAsync();


                return new ResponseDTO<IEnumerable<UserTasksDTO>>()
                {
                    Data = taskList,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<UserTasksDTO>>()
                {
                    Data = null,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<TaskDetailDTO>> GetByID(Guid id, Guid userID)
        {
            try
            {
                using var context = _context;

                if (await context.UsersInTasks.AnyAsync(d => d.TaskId.Equals(id) && d.UserId.Equals(userID)))
                {
                    Models.Task task = await context.Tasks.FindAsync(id);

                    List<UserDTO> list = await (from users in context.Users.AsNoTracking()
                                                join users_in_task in context.UsersInTasks.AsNoTracking()
                                                on users.id equals users_in_task.UserId
                                                where users_in_task.TaskId.Equals(id)
                                                select new UserDTO
                                                {
                                                    Name = users.UserName,
                                                    Email = users.Email,
                                                    Id = users.id,
                                                    Role = users_in_task.Role
                                                }).ToListAsync();

                    TaskDetailDTO taskDetailDTO = new TaskDetailDTO
                    {
                        CreatedAt = task.CreatedAt,
                        Description = task.Description,
                        id = task.id,
                        IsCompleted = task.IsCompleted,
                        Title = task.Title,
                        UpdatedAt = task.UpdatedAt,
                        Users = list,
                    };

                    return new ResponseDTO<TaskDetailDTO>
                    {
                        Data = taskDetailDTO,
                        Success = true
                    };
                }
                else
                {
                    throw new Exception("No existe esta tarea o no participas de ella.");
                }

            }
            catch (Exception ex)
            {
                return new ResponseDTO<TaskDetailDTO>
                {
                    Data = null,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<bool>> AddUser(AddUserInTaskDTO addUserInTaskDTO, Guid taskId, Guid ownerId)
        {
            try
            {
                using var context = _context;

                if (await context.UsersInTasks.AnyAsync(d => d.TaskId.Equals(taskId) && d.UserId.Equals(ownerId) && d.Role.Equals(Enums.Roles.OWNER)))
                {
                    if (!await context.UsersInTasks.AnyAsync(d => d.TaskId.Equals(taskId) && d.UserId.Equals(addUserInTaskDTO.UserId)))
                    {
                        UserInTask userInTask = new UserInTask
                        {
                            TaskId = taskId,
                            UserId = addUserInTaskDTO.UserId,
                            Role = addUserInTaskDTO.Role,
                        };

                        await context.AddAsync(userInTask);
                        await context.SaveChangesAsync();

                        return new ResponseDTO<bool>
                        {
                            Data = true,
                            Success = true,
                        };
                    }
                    else
                    {
                        throw new Exception("Este usuario ya forma parte de esta tarea");
                    }
                }
                else
                {
                    throw new Exception("No tienes permisos para invitar a un usuario");
                }

            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>
                {
                    Data = true,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<bool>> RemoveUser(AddUserInTaskDTO addUserInTaskDTO, Guid taskId, Guid ownerId)
        {
            try
            {
                using var context = _context;

                if (await context.UsersInTasks.AnyAsync(d => d.TaskId.Equals(taskId) && d.UserId.Equals(ownerId) && d.Role.Equals(Enums.Roles.OWNER)))
                {

                    await context.UsersInTasks.Where(d => d.TaskId.Equals(taskId) && d.UserId.Equals(addUserInTaskDTO.UserId))
                        .ExecuteDeleteAsync();

                    return new ResponseDTO<bool>
                    {
                        Data = true,
                        Success = true
                    };
                }
                else
                {
                    throw new Exception("No existe esta tarea o no eres el propietario.");
                }

            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>
                {
                    Data = false,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }
    }
}
