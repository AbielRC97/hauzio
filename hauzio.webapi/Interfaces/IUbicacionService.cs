using hauzio.webapi.DTOs;
using hauzio.webapi.Entities;

namespace hauzio.webapi.Interfaces
{
    public interface IUbicacionService
    {
        Task InsertLocation(UbicacionDTO ubicacionDTO);
        Task<List<Ubicacion>> FindAllLocations();
        Task<Ubicacion> DeleteByIdLocation(string id);
        Task<Ubicacion> FindByIdLocation(string id);
        Task<Ubicacion> UpdateByIDLocation(string id, UbicacionDTO ubicacionDTO);
        Task<List<Ubicacion>> FindByNameLocation(string negocio);
    }
}
