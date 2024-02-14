using TaskPro_back.Entities;
using TaskPro_back.Models.MongoModels;

namespace TaskPro_back.IRepository
{
    public interface ICommentRepository
    {
        public Task<ResponseDTO<Comment>> Create(Comment comment);
        public Task<ResponseDTO<IEnumerable<Comment>>> Get(Guid taskId);
        public Task<ResponseDTO<Comment>> Update(Comment comment, Guid id);
        public Task<ResponseDTO<Comment>> Delete(Guid id);
    }
}
