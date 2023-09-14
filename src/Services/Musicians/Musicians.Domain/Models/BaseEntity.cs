using MongoDB.Bson.Serialization.Attributes;

namespace Musicians.Domain.Models
{
    public abstract class BaseEntity
    {
        [BsonId, BsonElement("_id")]
        public Guid Id { get; set; }
    }
}
