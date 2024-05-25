using hauzio.webapi.DTOs;
using hauzio.webapi.Entities;

namespace hauzio.webapi.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllUsuarios();
        Task CreateUsuario(Usuario usuario);
        Task<Usuario> FindByIdUsuario(string id);
        Task UpdateByIdUsuario(string id, Usuario usuario);
        Task DeleteByIdUsuario(string id);
        Task<Usuario> FindByUsernameAndPassword(Login login);
    }
}
