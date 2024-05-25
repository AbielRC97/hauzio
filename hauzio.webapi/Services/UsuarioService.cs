using hauzio.webapi.Connections;
using hauzio.webapi.Entities;
using hauzio.webapi.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace hauzio.webapi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppConfig appConfig;
        private IMongoCollection<Usuario> usuarios
        {
            get
            {
                var mongoClient = new MongoClient(appConfig.Connection);
                var mongoDatabase = mongoClient.GetDatabase(appConfig.NameDB);
                var _usuariosCollection = mongoDatabase.GetCollection<Usuario>("Usuarios");
                return _usuariosCollection;
            }
        }
        public UsuarioService(IOptions<AppConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
            
        }

        public async Task CreateUsuario(Usuario usuario) => await usuarios.InsertOneAsync(usuario);

        public async Task<List<Usuario>> GetAllUsuarios() => await usuarios.Find(_ => true).ToListAsync();

        public async Task<Usuario> FindByIdUsuario(string id) => await usuarios.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateByIdUsuario(string id, Usuario usuario) => await usuarios.ReplaceOneAsync(x => x.Id == id, usuario);

        public async Task DeleteByIdUsuario(string id) => await usuarios.DeleteOneAsync(x => x.Id == id);
    }
}
