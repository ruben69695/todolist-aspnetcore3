using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class User : KeyBase
    {
        [BsonElement("user-id")]
        public string UserIdentifier { get; set; } 

        [BsonElement("name")]
        public string Name { get; set; }
    }
}