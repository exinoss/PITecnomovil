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
    [RoutePrefix("api/facturas")]
    public class FacturasController : ApiController
    {
        private readonly TecnomovilContext _context;

        public FacturasController()
        {
            _context = new TecnomovilContext();
        }

        // GET api/facturas
        [HttpGet, Route("")]
        public IEnumerable<Factura> Get()
        {
            return _context.Facturas
                           .Include(f => f.Cliente)
                           .Include(f => f.Venta)
                           .ToList();
        }

        // GET api/facturas/5
        [HttpGet, Route("{id:int}", Name = "GetFacturaById")]
        public IHttpActionResult Get(int id)
        {
            var factura = _context.Facturas
                                  .Include(f => f.Cliente)
                                  .Include(f => f.Venta)
                                  .FirstOrDefault(f => f.IdFactura == id);
            if (factura == null)
                return NotFound();
            return Ok(factura);
        }

        // POST api/facturas
        [HttpPost, Route("")]
        public IHttpActionResult Post(Factura factura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Facturas.Add(factura);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetFacturaById",
                new { id = factura.IdFactura },
                factura
            );
        }

        // PUT api/facturas/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Put(int id, Factura factura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            factura.IdFactura = id;
            var existing = _context.Facturas.Find(id);
            if (existing == null)
                return NotFound();

            existing.Numero = factura.Numero;
            existing.FechaEmision = factura.FechaEmision;
            existing.Subtotal = factura.Subtotal;
            existing.IVA = factura.IVA;
            existing.Total = factura.Total;
            existing.Estado = factura.Estado;
            existing.IdVenta = factura.IdVenta;
            existing.IdCliente = factura.IdCliente;

            _context.SaveChanges();
            return Ok(existing);
        }

        // DELETE api/facturas/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null)
                return NotFound();

            _context.Facturas.Remove(factura);
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