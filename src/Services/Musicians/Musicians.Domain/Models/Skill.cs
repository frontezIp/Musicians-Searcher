using MongoDB.Bson.Serialization.Attributes;

namespace Musicians.Domain.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Skill : BaseEntity
    {
        [BsonElement("name"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Name { get; set; } = null!;
    }
}
