using API_RESTful.Data;
using API_RESTful.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace API_RESTful.Controllers
{
    [RoutePrefix("api/ventas")]
    public class VentasController : ApiController
    {
        private readonly TecnomovilContext _context;

        public VentasController()
        {
            _context = new TecnomovilContext();
        }

        // GET api/ventas
        [HttpGet, Route("")]
        public IEnumerable<Venta> Get()
        {
            return _context.Ventas
                           .Include(v => v.Cliente)
                           .Include(v => v.Usuario)
                           .ToList();
        }

        // GET api/ventas/5
        [HttpGet, Route("{id:int}", Name = "GetVentaById")]
        public IHttpActionResult Get(int id)
        {
            var venta = _context.Ventas
                                .Include(v => v.Cliente)
                                .Include(v => v.Usuario)
                                .FirstOrDefault(v => v.IdVenta == id);
            if (venta == null)
                return NotFound();

            return Ok(venta);
        }

        // POST api/ventas
        [HttpPost, Route("")]
        public IHttpActionResult Post(Venta venta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Debug temporal: verificar el valor del Total que llega
            System.Diagnostics.Debug.WriteLine($"Total recibido en API: {venta.Total}");

            _context.Ventas.Add(venta);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetVentaById",
                new { id = venta.IdVenta },
                venta
            );
        }

        // PUT api/ventas/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Put(int id, Venta venta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            venta.IdVenta = id;
            var existing = _context.Ventas.Find(id);
            if (existing == null)
                return NotFound();

            existing.Fecha = venta.Fecha;
            existing.Total = venta.Total;
            existing.IdCliente = venta.IdCliente;
            existing.IdUsuario = venta.IdUsuario;
            _context.SaveChanges();

            return Ok(existing);
        }

        // DELETE api/ventas/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var venta = _context.Ventas.Find(id);
            if (venta == null)
                return NotFound();

            _context.Ventas.Remove(venta);
            _context.SaveChanges();
            return Ok();
        }
    }
}