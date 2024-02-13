using TaskPro_back.Entities;
using TaskPro_back.Models;

namespace TaskPro_back.IRepository
{
    public interface ITaskRepository
    {
        public Task<ResponseDTO<IEnumerable<UserTasksDTO>>> Get(Guid userId);
        public Task<ResponseDTO<TaskDetailDTO>> GetByID(Guid id, Guid userID);
        public Task<ResponseDTO<Models.Task>> Create(TaskDTO taskDTO, Guid userId);
        public Task<ResponseDTO<Models.Task>> Update(TaskDTO taskDTO, Guid id, Guid userId);
        public Task<ResponseDTO<Models.Task>> Delete(Guid id, Guid userId);
        public Task<ResponseDTO<bool>> AddUser(AddUserInTaskDTO addUserInTaskDTO, Guid taskId, Guid ownerId);
        public Task<ResponseDTO<bool>> RemoveUser(AddUserInTaskDTO addUserInTaskDTO, Guid taskId, Guid ownerId);
    }
}
