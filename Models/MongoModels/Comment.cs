using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Models.MongoModels
{
    public class Comment
    {

        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        [BsonId]
        public Guid Id { get; set; }


        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        public Guid TaskId { get; set; }


        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
        public Guid UserId { get; set; }


        public string UserName { get; set; }

        public string Text { get; set; }
    }
}
