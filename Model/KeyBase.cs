using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class KeyBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}