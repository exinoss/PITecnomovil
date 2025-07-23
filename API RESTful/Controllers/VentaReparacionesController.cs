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
    [RoutePrefix("api/ventareparaciones")]
    public class VentaReparacionesController : ApiController
    {
        private readonly TecnomovilContext _context;

        public VentaReparacionesController()
        {
            _context = new TecnomovilContext();
        }

        // GET api/ventareparaciones
        [HttpGet, Route("")]
        public IEnumerable<VentaReparacion> Get()
        {
            return _context.VentaReparaciones
                           .Include(vr => vr.Venta)
                           .Include(vr => vr.Reparacion)
                           .ToList();
        }

        // GET api/ventareparaciones/5/3
        [HttpGet, Route("{idVenta:int}/{idReparacion:int}", Name = "GetVentaReparacionById")]
        public IHttpActionResult Get(int idVenta, int idReparacion)
        {
            var vr = _context.VentaReparaciones
                             .Include(x => x.Venta)
                             .Include(x => x.Reparacion)
                             .FirstOrDefault(x => x.IdVenta == idVenta
                                               && x.IdReparacion == idReparacion);
            if (vr == null)
                return NotFound();
            return Ok(vr);
        }

        // POST api/ventareparaciones
        [HttpPost, Route("")]
        public IHttpActionResult Post(VentaReparacion ventaReparacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.VentaReparaciones.Add(ventaReparacion);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetVentaReparacionById",
                new { idVenta = ventaReparacion.IdVenta, idReparacion = ventaReparacion.IdReparacion },
                ventaReparacion
            );
        }

        // PUT api/ventareparaciones/5/3
        [HttpPut, Route("{idVenta:int}/{idReparacion:int}")]
        public IHttpActionResult Put(int idVenta, int idReparacion, VentaReparacion ventaReparacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Forzamos claves según la ruta
            ventaReparacion.IdVenta = idVenta;
            ventaReparacion.IdReparacion = idReparacion;

            var existing = _context.VentaReparaciones.Find(idVenta, idReparacion);
            if (existing == null)
                return NotFound();

            existing.Subtotal = ventaReparacion.Subtotal;
            _context.SaveChanges();

            return Ok(existing);
        }

        // DELETE api/ventareparaciones/5/3
        [HttpDelete, Route("{idVenta:int}/{idReparacion:int}")]
        public IHttpActionResult Delete(int idVenta, int idReparacion)
        {
            var vr = _context.VentaReparaciones.Find(idVenta, idReparacion);
            if (vr == null)
                return NotFound();

            _context.VentaReparaciones.Remove(vr);
            _context.SaveChanges();
            return Ok();
        }
    }
}