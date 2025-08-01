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
    [RoutePrefix("api/clientes")]
    public class ClienteController : ApiController
    {
        private readonly TecnomovilContext _context;

        public ClienteController()
        {
            _context = new TecnomovilContext();
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Cliente> Get()
        {
            return _context.Clientes.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Clientes.Add(cliente);
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
                                   "La cédula ya está registrada en otro cliente.");
                }
                throw; // no sabemos qué fue, relanzar
            }
            return CreatedAtRoute("DefaultApi", new { controller = "clientes", id = cliente.IdCliente }, cliente);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cliente.IdCliente = id;


            var existingCliente = _context.Clientes.Find(id);
            if (existingCliente == null)
                return NotFound();

            existingCliente.Nombres = cliente.Nombres;
            existingCliente.Cedula = cliente.Cedula;
            existingCliente.Contacto = cliente.Contacto;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlEx = ex.InnerException?.InnerException as SqlException;
                if (sqlEx != null && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    return Content(HttpStatusCode.Conflict,
                                   "La cédula ya está registrada en otro cliente.");
                }
                throw;
            }
            return Ok(existingCliente);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok();
        }

        //GET api/<controller>/search?nombre=
        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search(string nombre)
        {
            var nombres = _context.Clientes
                .Where(p => p.Nombres.Contains(nombre ?? ""))
                .ToList();
            return Ok(nombres);
        }
    }
}