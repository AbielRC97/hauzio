using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using hauzio.webapi.DTOs;

namespace hauzio.webapi.Entities
{
    public class Ubicacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("negocio")]
        public string negocio { get; set; }
        [BsonElement("descripcion")]
        public string descripcion { get; set; }
        [BsonElement("latitud")]
        public double latitud { get; set; }
        [BsonElement("longitud")]
        public double longitud { get; set; }
        [BsonElement("Estatus")]
        public bool status { get; set; }
        public virtual UbicacionDTO NewDTO() => new UbicacionDTO()
        {
            descripcion = this.descripcion,
            latitud = this.latitud,
            longitud = this.longitud,
            status = this.status,
            negocio = this.negocio
        };
    }
}
