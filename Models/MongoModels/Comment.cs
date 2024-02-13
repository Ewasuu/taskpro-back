using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Models.MongoModels
{
    public class Comment
    {
        [Required]
        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        [BsonId]
        public Guid Id { get; set; }

        [Required]
        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        public Guid TaskId { get; set; }

        [Required]
        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        public Guid UserId { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
