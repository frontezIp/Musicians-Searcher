using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MongoDB.Bson.Serialization.Attributes;
using Musicians.Domain.Models;
using Shared.Enums;

namespace Musicians.Infrastructure.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Musician
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = null!;

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
        public SexTypes Sex { get; set; }

        [BsonElement("goal"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Goal { get; set; } = string.Empty;

        [BsonElement("biography"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Biography { get;set; } = string.Empty;

        [BsonElement("created_at"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("subscribers")]
        public List<Musician> Subscribers { get; set; } = new();

        [BsonElement("friends")]
        public List<Musician> Friends { get; set; } = new();

        [BsonElement("favourite_genres")]
        public List<Genre> FavouriteGenres { get; set; } = new();

        [BsonElement("skills")]
        public List<Genre> Skills { get; set; } = new();
    }
}
