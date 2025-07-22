using API_RESTful.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_RESTful.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private readonly TecnomovilContext _context;

        public LoginController()
        {
            _context = new TecnomovilContext();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] LoginRequest login)
        {
            if (login == null || string.IsNullOrEmpty(login.NombreUsuario) || string.IsNullOrEmpty(login.Clave))
                return BadRequest("El nombre de usuario y la clave son obligatorios.");

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario.Equals(login.NombreUsuario) && u.Clave == login.Clave);

            if (usuario == null)
                return Unauthorized();

            return Ok(new { Message = "Login exitoso", Rol = usuario.Rol });
        }
    }

    public class LoginRequest
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
    }
}