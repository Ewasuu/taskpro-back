using TaskPro_back.Entities;

namespace TaskPro_back.IRepository
{
    public interface ITaskRepository
    {
        public Task<ResponseDTO<IEnumerable<UserTasksDTO>>> Get(string filter, Guid userId);
        public Task<ResponseDTO<Models.Task>> GetByID(Guid id, Guid userID);
        public Task<ResponseDTO<Models.Task>> Create(TaskDTO taskDTO, Guid userId);
        public Task<ResponseDTO<Models.Task>> Update(TaskDTO taskDTO, Guid userId);
        public Task<ResponseDTO<Models.Task>> Delete(Guid id, Guid userId);
        public Task<ResponseDTO<bool>> InviteUser(Guid taskId, Guid userId);
        public Task<ResponseDTO<bool>> RevokeUser(Guid taskId, Guid userId, Guid ownerId);
    }
}
