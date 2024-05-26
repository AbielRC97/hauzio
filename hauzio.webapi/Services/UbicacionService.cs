using hauzio.webapi.Connections;
using hauzio.webapi.DTOs;
using hauzio.webapi.Entities;
using hauzio.webapi.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace hauzio.webapi.Services
{
    public class UbicacionService : IUbicacionService
    {
        private readonly AppConfig appConfig;
        private IMongoCollection<Ubicacion> locations
        {
            get
            {
                var mongoClient = new MongoClient(appConfig.Connection);
                var mongoDatabase = mongoClient.GetDatabase(appConfig.NameDB);
                var _locationsCollection = mongoDatabase.GetCollection<Ubicacion>("Ubicaciones");
                return _locationsCollection;
            }
        }
        public UbicacionService(IOptions<AppConfig> appConfig)
        {
            this.appConfig = appConfig.Value;

        }
        public async Task InsertLocation(UbicacionDTO ubicacionDTO)
        {
            Ubicacion nueva = new Ubicacion();
            nueva.Id = ObjectId.GenerateNewId().ToString();
            nueva.descripcion = ubicacionDTO.descripcion;
            nueva.status = true;
            nueva.latitud = ubicacionDTO.latitud;
            nueva.longitud = ubicacionDTO.longitud;
            nueva.negocio = ubicacionDTO.negocio;
            await locations.InsertOneAsync(nueva);
        }

        public async Task<List<Ubicacion>> FindAllLocations() => await locations.Find(_ => _.status).ToListAsync();

        public async Task<Ubicacion> DeleteByIdLocation(string id)
        {
            Ubicacion ubicacionDB = await FindByIdLocation(id);
            await locations.DeleteOneAsync(x => x.Id == id);
            return ubicacionDB;
        }

        public async Task<Ubicacion> FindByIdLocation(string id)=> await locations.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Ubicacion> UpdateByIDLocation(string id, UbicacionDTO ubicacionDTO)
        {
            Ubicacion locationDB = ubicacionDTO.BuildLocation(id);
            if (!locationDB.status)
            {
                locationDB = await FindByIdLocation(id);
                locationDB.status = false;
            }
            await locations.ReplaceOneAsync(x => x.Id == id, locationDB);
            return locationDB;
        }

        public async Task<List<Ubicacion>> FindByNameLocation(string negocio) => await locations.Find(x => x.negocio.ToLower().Contains(negocio.ToLower())).ToListAsync();    
    }
}

