using MongoDB.Bson.Serialization.Attributes;

namespace Musicians.Domain.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Skill
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("name"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Name { get; set; } = null!;
    }
}
