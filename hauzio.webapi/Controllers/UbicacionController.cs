using hauzio.webapi.DTOs;
using hauzio.webapi.Entities;
using hauzio.webapi.Interfaces;
using hauzio.webapi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hauzio.webapi.Controllers
{
    public class UbicacionController : JwtBaseController
    {
        private readonly ILogger<UbicacionController> _logger;
        private readonly IUbicacionService _ubicacionService;
        private readonly IUsuarioService _userDB;
        public UbicacionController(ILogger<UbicacionController> logger, IUbicacionService ubicacionService, IUsuarioService userDB)
        {
            _logger = logger;
            _ubicacionService = ubicacionService;
            _userDB = userDB;
        }

        [HttpGet]
        [Route("/locations")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/Admin")]
        public IActionResult Register(string id = null)
        {
            return View();
        }

        [HttpGet]
        [Route("/api/FindById")]
        [Authorize]
        public async Task<Ubicacion> FindById(string id)
        {
            return await _ubicacionService.FindByIdLocation(id);
        }

        [HttpPost("/api/Insert")]
        [Authorize]
        public async Task  insertLocation([FromBody] UbicacionDTO ubicacionDTO)
        {
            Usuario userDB = await _userDB.FindByIdUsuario(userID);
            if(userDB != null && (userDB?.status ?? false))
            {
                await _ubicacionService.InsertLocation(ubicacionDTO);
            }
        }

        [HttpGet("/api/GetAll")]
        [Authorize]
        public async Task<List<Ubicacion>> FindAllLocations()
        {
            Usuario userDB = await _userDB.FindByIdUsuario(userID);
            if (userDB != null && (userDB?.status ?? false))
            {
                return (await _ubicacionService.FindAllLocations());
            }
            return new List<Ubicacion>();
        }
        [HttpGet("/api/FindByName")]
        [Authorize]
        public async Task<List<Ubicacion>> FindAllLocations(string? name)
        {
            Usuario userDB = await _userDB.FindByIdUsuario(userID);
            if (userDB != null && (userDB?.status ?? false))
            {
                return (await _ubicacionService.FindAllLocations());
            }
            return new List<Ubicacion>();
        }

        [HttpPost("/api/FullActions")]
        [Authorize]
        public async Task<Ubicacion> AllActionsLocations(string? id,[FromBody] UbicacionDTO? ubicacionDTO = null)
        {
            Usuario userDB = await _userDB.FindByIdUsuario(userID);
            if (userDB != null && (userDB?.status ?? false))
            {
                if(!string.IsNullOrEmpty(id) && ( string.IsNullOrEmpty(ubicacionDTO?.negocio) || string.IsNullOrEmpty(ubicacionDTO.descripcion)))
                    return (await _ubicacionService.DeleteByIdLocation(id!));
                else if(!string.IsNullOrEmpty(id) && ubicacionDTO != null)
                    return (await _ubicacionService.UpdateByIDLocation(id!, ubicacionDTO)); 
            }
            return new Ubicacion();
        }
    }
}
