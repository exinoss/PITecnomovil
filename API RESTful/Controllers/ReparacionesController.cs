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
    [RoutePrefix("api/reparaciones")]
    public class ReparacionesController : ApiController
    {
        private readonly TecnomovilContext _context;
        public ReparacionesController()
        {
            _context = new TecnomovilContext();
        }

        // GET api/reparaciones
        [HttpGet, Route("")]
        public IEnumerable<Reparacion> Get()
        {
            // Incluimos Cliente y Usuario si queremos datos completos
            return _context.Reparaciones
                           .Include(r => r.Cliente)
                           .Include(r => r.Usuario)
                           .ToList();
        }

        // GET api/reparaciones/5
        [HttpGet, Route("{id:int}", Name = "GetReparacionById")]
        public IHttpActionResult Get(int id)
        {
            var reparacion = _context.Reparaciones
                                     .Include(r => r.Cliente)
                                     .Include(r => r.Usuario)
                                     .FirstOrDefault(r => r.IdReparacion == id);
            if (reparacion == null)
                return NotFound();
            return Ok(reparacion);
        }

        // POST api/reparaciones
        [HttpPost, Route("")]
        public IHttpActionResult Post(Reparacion reparacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Reparaciones.Add(reparacion);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetReparacionById",
                new { id = reparacion.IdReparacion },
                reparacion
            );
        }

        // PUT api/reparaciones/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Put(int id, Reparacion reparacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            reparacion.IdReparacion = id;
            var existing = _context.Reparaciones.Find(id);
            if (existing == null)
                return NotFound();

            existing.IdCliente = reparacion.IdCliente;
            existing.IdUsuario = reparacion.IdUsuario;
            existing.FechaIngreso = reparacion.FechaIngreso;
            existing.FechaEntrega = reparacion.FechaEntrega;
            existing.Estado = reparacion.Estado;
            existing.Diagnostico = reparacion.Diagnostico;
            existing.Observaciones = reparacion.Observaciones;
            existing.Dispositivo = reparacion.Dispositivo;
            existing.PrecioServicio = reparacion.PrecioServicio;

            _context.SaveChanges();
            return Ok(existing);
        }

        // DELETE api/reparaciones/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var reparacion = _context.Reparaciones.Find(id);
            if (reparacion == null)
                return NotFound();

            _context.Reparaciones.Remove(reparacion);
            _context.SaveChanges();
            return Ok();
        }

        // GET api/reparaciones/cliente/{idCliente}/unpaid
        [HttpGet, Route("cliente/{idCliente:int}/unpaid")]
        public IHttpActionResult GetUnpaidRepairsByClient(int idCliente)
        {
            try
            {
                // Obtener reparaciones del cliente que no han sido vendidas o que están pendientes de pago
                var unpaidRepairs = _context.Reparaciones
                    .Include(r => r.Cliente)
                    .Include(r => r.Usuario)
                    .Where(r => r.IdCliente == idCliente && 
                               r.Estado == "Entregado" && // Solo reparaciones entregadas
                               !_context.VentaReparaciones.Any(vr => vr.IdReparacion == r.IdReparacion && 
                                                                     vr.EstadoPago == "PAGADO"))
                    .ToList();

                return Ok(unpaidRepairs);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}