using hauzio.webapi.DTOs;
using hauzio.webapi.Entities;
using hauzio.webapi.Interfaces;
using hauzio.webapi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<RespuestaDTO<Ubicacion>> FindById(string id)
        {
            try
            {
                Usuario userDB = await _userDB.FindByIdUsuario(userID);
                if (userDB != null && (userDB?.status ?? false))
                {
                    var ubicacionDB = await _ubicacionService.FindByIdLocation(id);
                    return new RespuestaDTO<Ubicacion>()
                    {
                        Data = ubicacionDB,
                        Error = null,
                        Estatus = true
                    };
                }
                throw new Exception("Ocurrio un error!");
            }
            catch (Exception ex)
            {
                return new RespuestaDTO<Ubicacion>()
                {
                    Data = null,
                    Error = ex.Message,
                    Estatus = false,
                    Mensaje = "No se encontro la ubicacion"
                };
            }
        }

        [HttpPost("/api/Insert")]
        [Authorize]
        public async Task<RespuestaDTO<string>> insertLocation([FromBody] UbicacionDTO ubicacionDTO)
        {
            try
            {
                Usuario userDB = await _userDB.FindByIdUsuario(userID);
                if (userDB != null && (userDB?.status ?? false))
                {
                    await _ubicacionService.InsertLocation(ubicacionDTO);
                    return new RespuestaDTO<string>
                    {
                        Data = $"Se guardo correctamente la ubicacion {ubicacionDTO.negocio}",
                        Error = string.Empty,
                        Estatus = true
                    };
                }
                throw new Exception("Ocurrio un error al guardar la ubicacion");
            }
            catch (Exception ex)
            {
                return new RespuestaDTO<string>
                {
                    Data = string.Empty,
                    Error = ex.Message,
                    Estatus = true
                };
            }
        }

        [HttpGet("/api/GetAll")]
        [Authorize]
        public async Task<List<Ubicacion>> FindAllLocations()
        {
            try
            {
                Usuario userDB = await _userDB.FindByIdUsuario(userID);
                if (userDB != null && (userDB?.status ?? false))
                {
                    var ubicaiconesDB = await _ubicacionService.FindAllLocations();
                    return ubicaiconesDB;
                }
                throw new Exception();
            }
            catch (Exception)
            {
                return new List<Ubicacion>();
            }
        }
        [HttpGet("/api/FindByName")]
        [Authorize]
        public async Task<List<Ubicacion>> FindAllLocations(string? name)
        {
            try
            {
                Usuario userDB = await _userDB.FindByIdUsuario(userID);
                if (userDB != null && (userDB?.status ?? false))
                {
                    return (await _ubicacionService.FindAllLocations());
                }
                throw new Exception();
            }
            catch (Exception)
            {
                return new List<Ubicacion>();
            }
        }

        [HttpPost("/api/FullActions")]
        [Authorize]
        public async Task<RespuestaDTO<Ubicacion>> AllActionsLocations(string? id, [FromBody] UbicacionDTO? ubicacionDTO = null)
        {
            try
            {
                Usuario userDB = await _userDB.FindByIdUsuario(userID);
                if (userDB != null && (userDB?.status ?? false))
                {
                    if (!string.IsNullOrEmpty(id) && (string.IsNullOrEmpty(ubicacionDTO?.negocio) || string.IsNullOrEmpty(ubicacionDTO.descripcion)))
                    {
                        var ubicacionDB = await _ubicacionService.DeleteByIdLocation(id!);
                        return new RespuestaDTO<Ubicacion>()
                        {
                            Data = ubicacionDB,
                            Mensaje = $"Se elimino ubicacion {ubicacionDB.negocio} correctamente",
                            Error = string.Empty,
                            Estatus = true
                        };
                    }
                    else if (!string.IsNullOrEmpty(id) && ubicacionDTO != null)
                    {
                        var ubicacionDB = await _ubicacionService.UpdateByIDLocation(id!, ubicacionDTO);
                        return new RespuestaDTO<Ubicacion>()
                        {
                            Data = ubicacionDB,
                            Mensaje = $"Se Actualizo ubicacion {ubicacionDB.negocio} correctamente",
                            Error = string.Empty,
                            Estatus = true
                        };
                    }
                }
                throw new Exception("Ocurrio un error");
            }
            catch (Exception ex)
            {
                return new RespuestaDTO<Ubicacion>()
                {
                    Data = null,
                    Error = ex.Message,
                    Estatus = false,
                    Mensaje = "Ocurrio algo al procesar la informacion"
                };
            }
        }
    }
}
