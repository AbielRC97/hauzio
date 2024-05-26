
using hauzio.webapi.Entities;
using Newtonsoft.Json;

namespace hauzio.webapi.DTOs
{
    public class UbicacionDTO
    {
        public string negocio { get; set; }
        public string descripcion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public bool status { get; set; }
        public virtual Ubicacion BuildLocation(string id) => new Ubicacion
        {
            Id = id,
            descripcion = this.descripcion,
            latitud = this.latitud,
            longitud = this.longitud,   
            status = this.status,
            negocio = this.negocio
        };
        public virtual bool EsValido() => !string.IsNullOrEmpty(this.negocio) && !string.IsNullOrEmpty(descripcion);
    }
}
