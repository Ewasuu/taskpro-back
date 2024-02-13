using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskPro_back.Entities;
using TaskPro_back.IRepository;
using TaskPro_back.Models.MongoModels;

namespace TaskPro_back.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private readonly IMongoCollection<Comment> _commentsCollection;

        public CommentRepository(IOptions<CommentDatabaseConfiguration> commentConfiguration)
        {
            var mongoClient = new MongoClient(commentConfiguration.Value.ConnectionString);

            var taskPro = mongoClient.GetDatabase(commentConfiguration.Value.DatabaseName);

            _commentsCollection = taskPro.GetCollection<Comment>(commentConfiguration.Value.CommentCollectionName);
        }


        public async Task<ResponseDTO<Comment>> Create(Comment comment)
        {
            try
            {
                comment.Id = Guid.NewGuid();
                await _commentsCollection.InsertOneAsync(comment);

                return new ResponseDTO<Comment>
                {
                    Data = comment,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Comment>
                {
                    Data = null,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public Task<ResponseDTO<Comment>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO<IEnumerable<Comment>>> Get(Guid taskId)
        {
            try
            {

                IEnumerable<Comment> commentList = await _commentsCollection.Find(Builders<Comment>.Filter.Empty).ToListAsync();
                commentList = commentList.Where(x => x.TaskId.Equals(taskId)).ToList();

                return new ResponseDTO<IEnumerable<Comment>>
                {
                    Data = commentList,
                    Success = true,

                };
            }
            catch (Exception ex)
            {

                return new ResponseDTO<IEnumerable<Comment>>
                {
                    Data = null,
                    Success = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public Task<ResponseDTO<Comment>> Update(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
