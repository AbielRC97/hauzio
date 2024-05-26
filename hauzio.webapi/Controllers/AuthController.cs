using hauzio.webapi.DTOs;
using hauzio.webapi.Entities;
using hauzio.webapi.Interfaces;
using hauzio.webapi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace hauzio.webapi.Controllers
{

    [ApiController]
    public class AuthController : JwtBaseController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUsuarioService _userDB;
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;
        public AuthController(ILogger<AuthController> logger, IUsuarioService userDB, IConfiguration configuration, ISecurityService securityService)
        {
            _logger = logger;
            _userDB = userDB;
            _configuration = configuration;
            _securityService = securityService;
        }

        [HttpGet("/api/HasSession")]
        public async Task<bool> HasSession()
        {
            await Task.Yield();
            return !string.IsNullOrEmpty(userID);
        }
        [HttpPost("/api/InsertUser")]
        [Authorize]
        public async Task<Usuario> UsuarioAsync(Usuario usuario)
        {
            usuario.Id = ObjectId.GenerateNewId().ToString();
            usuario.password = _securityService.CifrarTexto(usuario.password);
            await _userDB.CreateUsuario(usuario);
            return usuario;
        }

        [HttpPost("/api/login")]
        public async Task<object> LoginAsync([FromBody] Login login)
        {
            try
            {
                login.password = _securityService.CifrarTexto(login.password);
                Usuario usuario = await _userDB.FindByUsernameAndPassword(login);
                string token = GenerateJwtToken(usuario?.Id!);
                return new RespuestaDTO<string>
                {
                    Data = token,
                    Error = string.Empty,
                    Estatus = true
                };
            }
            catch (Exception ex)
            {
                return new RespuestaDTO<string>
                {
                    Data = string.Empty,
                    Error = "Credenciales no validas",
                    Estatus = false
                };
            }
        }
        [HttpPut("{id:length(24)}")]
        [Authorize]
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
        [Authorize]
        public async Task<Usuario> Delete(string id)
        {
            var userDB = await _userDB.FindByIdUsuario(id);

            if (userDB is null)
                return null;
            await _userDB.DeleteByIdUsuario(id);
            return userDB;
        }

        private string GenerateJwtToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiresInMinutes"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}
