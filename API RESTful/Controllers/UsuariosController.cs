﻿using API_RESTful.Data;
using API_RESTful.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_RESTful.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private readonly TecnomovilContext _context;

        public UsuariosController()
        {
            _context = new TecnomovilContext();
        }

        // GET api/usuarios
        [HttpGet, Route("")]
        public IEnumerable<Usuario> Get()
        {
            return _context.Usuarios.ToList();
        }

        // GET api/usuarios/5
        [HttpGet, Route("{id:int}", Name = "GetUsuarioById")]
        public IHttpActionResult Get(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        // POST api/usuarios
        [HttpPost, Route("")]
        public IHttpActionResult Post(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Usuarios.Add(usuario);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlEx = ex.InnerException?.InnerException as SqlException;
                if (sqlEx != null && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    // Violación de índice único en Cedula
                    return Content(HttpStatusCode.Conflict,
                                   "El nombre de usuario ya está registrada en otro usuario.");
                }
                throw; // no sabemos qué fue, relanzar
            }

            return CreatedAtRoute(
                "GetUsuarioById",
                new { id = usuario.IdUsuario },
                usuario
            );
        }

        // PUT api/usuarios/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Put(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            usuario.IdUsuario = id;
            var existing = _context.Usuarios.Find(id);
            if (existing == null)
                return NotFound();

            existing.NombreUsuario = usuario.NombreUsuario;
            existing.Clave = usuario.Clave;
            existing.Rol = usuario.Rol;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlEx = ex.InnerException?.InnerException as SqlException;
                if (sqlEx != null && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    // Violación de índice único en Cedula
                    return Content(HttpStatusCode.Conflict,
                                   "El nombre de usuario ya está registrada en otro usuario.");
                }
                throw; // no sabemos qué fue, relanzar
            }

            return Ok(existing);
        }

        // DELETE api/usuarios/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok();
        }
    }
}