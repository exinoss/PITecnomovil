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
    [RoutePrefix("api/pagos")]
    public class PagosController : ApiController
    {
        private readonly TecnomovilContext _context;

        public PagosController()
        {
            _context = new TecnomovilContext();
        }

        // GET api/pagos
        [HttpGet, Route("")]
        public IEnumerable<Pago> Get()
        {
            return _context.Pagos
                           .Include(p => p.Factura)
                           .ToList();
        }

        // GET api/pagos/5
        [HttpGet, Route("{id:int}", Name = "GetPagoById")]
        public IHttpActionResult Get(int id)
        {
            var pago = _context.Pagos
                               .Include(p => p.Factura)
                               .FirstOrDefault(p => p.IdPago == id);
            if (pago == null)
                return NotFound();
            return Ok(pago);
        }

        // POST api/pagos
        [HttpPost, Route("")]
        public IHttpActionResult Post(Pago pago)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Pagos.Add(pago);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetPagoById",
                new { id = pago.IdPago },
                pago
            );
        }

        // PUT api/pagos/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Put(int id, Pago pago)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            pago.IdPago = id;
            var existing = _context.Pagos.Find(id);
            if (existing == null)
                return NotFound();

            existing.Fecha = pago.Fecha;
            existing.Monto = pago.Monto;
            existing.Metodo = pago.Metodo;
            existing.IdFactura = pago.IdFactura;

            _context.SaveChanges();
            return Ok(existing);
        }

        // DELETE api/pagos/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var pago = _context.Pagos.Find(id);
            if (pago == null)
                return NotFound();

            _context.Pagos.Remove(pago);
            _context.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}