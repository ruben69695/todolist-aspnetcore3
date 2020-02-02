using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class User : KeyBase
    {
        [BsonElement("user-code")]
        public string Code { get; set; } 

        [BsonElement("name")]
        public string Name { get; set; }
    }
}