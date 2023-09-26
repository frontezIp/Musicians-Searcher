using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Enums;

namespace Musicians.Domain.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Musician : BaseEntity
    {
        [BsonElement("username"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Username { get; set; }  = string.Empty;

        [BsonElement("full_name"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string FullName { get; set; } = null!;

        [BsonElement("photo"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Photo { get; set; } = null!;

        [BsonElement("location"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Location { get; set; } = null!;

        [BsonElement("age"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Age { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [BsonElement("sex"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public SexTypes SexTypeId { get; set; }

        [BsonElement("goal"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Goal { get; set; } = string.Empty;

        [BsonElement("biography"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Biography { get;set; } = string.Empty;

        [BsonElement("created_at"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [BsonElement("subscribers_count"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int SubscribersCount { get; set; }
        [BsonElement("friends_count"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int FriendsCount { get; set; }

        [BsonElement("subscribers")]
        public List<Guid> Subscribers { get; set; } = new();

        [BsonElement("friends")]
        public List<Guid> Friends { get; set; } = new();

        [BsonElement("favourite_genres")]
        public List<Genre> FavouriteGenres { get; set; } = new();

        [BsonElement("skills")]
        public List<Skill> Skills { get; set; } = new();
    }
}
