using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Models.MongoModels
{
    public class Comment
    {
        [Required]
        [BsonId]
        public Guid Id { get; set; }

        [Required]
        public Guid TaskId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
