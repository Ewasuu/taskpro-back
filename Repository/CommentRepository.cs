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

        public CommentRepository(IOptions<CommentDatabaseConfiguration> commentConfiguration) {
            var mongoClient = new MongoClient(commentConfiguration.Value.ConnectionString);

            var taskPro = mongoClient.GetDatabase(commentConfiguration.Value.DatabaseName);

            _commentsCollection = taskPro.GetCollection<Comment>(commentConfiguration.Value.CommentCollectionName);
        }


        public async Task<ResponseDTO<Comment>> Create(Comment comment)
        {
            try
            {
                await _commentsCollection.InsertOneAsync(comment);

                return new ResponseDTO<Comment>
                {
                    Data = comment,
                    Succes = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Comment>
                {
                    Data = null,
                    Succes = false,
                    ErrorMesage = ex.Message
                };
            }
        }

        public Task<ResponseDTO<Comment>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<IEnumerable<Comment>>> Get(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<Comment>> Update(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
