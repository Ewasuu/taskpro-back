using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
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

        public async Task<ResponseDTO<Comment>> Delete(Guid id)
        {
            try
            {
                var filter = Builders<Comment>.Filter.Eq(comment => comment.Id, id);
                await _commentsCollection.FindOneAndDeleteAsync<Comment>(filter);

                return new ResponseDTO<Comment>
                {
                    Data = null,
                    Success = true,

                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Comment>
                {
                    Data = null,
                    Success = true,
                    ErrorMesage = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<Comment>>> Get(Guid taskId)
        {
            try
            {
                var filter = Builders<Comment>.Filter.Eq(comment => comment.TaskId, taskId);
                IEnumerable<Comment> commentList = await _commentsCollection.Find(filter).ToListAsync();

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
