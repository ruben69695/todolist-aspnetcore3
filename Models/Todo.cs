using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Todo : KeyBase
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("completed")]
        public bool Completed { get; set; }

        [BsonElement("user-id")]
        public string UserIdentifier { get; set; }
    }
}