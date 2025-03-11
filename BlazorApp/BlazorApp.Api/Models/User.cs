using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorApp.Api.Models
{
    public class User
    {
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("Role")]
        public string Role { get; set; } = "USER";
    }
}
