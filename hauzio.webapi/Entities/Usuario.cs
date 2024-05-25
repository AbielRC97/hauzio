using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace hauzio.webapi.Entities
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("UserName")]
        public string userName { get; set; }
        [BsonElement("Password")]
        public string password { get; set; }
        [BsonElement("Estatus")]
        public bool status {  get; set; }
    }
}
