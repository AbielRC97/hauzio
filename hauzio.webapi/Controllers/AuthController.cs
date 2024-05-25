using hauzio.webapi.Connections;
using hauzio.webapi.Entities;
using hauzio.webapi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace hauzio.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUsuarioService _userDB;
        public AuthController(ILogger<AuthController> logger, IUsuarioService userDB)
        {
            _logger = logger;
            _userDB = userDB;
        }
        [HttpGet("/{id:length(24)}")]
        public async Task<Usuario> GetUsuario(string id) => await _userDB.FindByIdUsuario(id);
        [HttpGet()]
        public async Task<List<Usuario>> GetAllUsuarios()
        {
            return await _userDB.GetAllUsuarios();
        }
        [HttpPost()]
        public async Task<Usuario> UsuarioAsync(Usuario usuario)
        {
            usuario.Id = ObjectId.GenerateNewId().ToString(); 
            await _userDB.CreateUsuario(usuario);
            return usuario;
        }
        [HttpPut("{id:length(24)}")]
        public async Task<Usuario> Update(string id, Usuario user)
        {
            var userDB = await _userDB.FindByIdUsuario(id);

            if (userDB is null)
                return null;

            user.Id = userDB.Id;

            await _userDB.UpdateByIdUsuario(id, user);
            return user;
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<Usuario> Delete(string id)
        {
            var userDB = await _userDB.FindByIdUsuario(id);

            if (userDB is null)
                return null;
            await _userDB.DeleteByIdUsuario(id);
            return userDB;
        }
    }
}
