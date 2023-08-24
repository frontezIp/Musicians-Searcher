using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicians.Domain.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Genre
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("name"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Name { get; set; } = null!;
    }
}
