﻿using TaskPro_back.Entities;

namespace TaskPro_back.IRepository
{
    public interface ITaskRepository
    {
        public Task<ResponseDTO<IEnumerable<UserTasksDTO>>> Get(string filter, Guid userId);
        public Task<ResponseDTO<Models.Task>> GetByID(Guid id, Guid userID);
        public Task<ResponseDTO<Models.Task>> Create(TaskDTO taskDTO, Guid userId);
        public Task<ResponseDTO<Models.Task>> Update(TaskDTO taskDTO, Guid id, Guid userId);
        public Task<ResponseDTO<Models.Task>> Delete(Guid id, Guid userId);
        public Task<ResponseDTO<bool>> AddUser(AddUserInTaskDTO addUserInTaskDTO, Guid taskId, Guid ownerId);
        public Task<ResponseDTO<bool>> RemoveUser(AddUserInTaskDTO addUserInTaskDTO, Guid taskId, Guid ownerId);
    }
}
