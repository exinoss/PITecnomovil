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
    [RoutePrefix("api/ventaproductos")]
    public class VentaProductosController : ApiController
    {
        private readonly TecnomovilContext _context;

        public VentaProductosController()
        {
            _context = new TecnomovilContext();
        }

        // GET api/ventaproductos
        [HttpGet, Route("")]
        public IEnumerable<VentaProducto> Get()
        {
            return _context.VentaProductos
                           .Include(vp => vp.Venta)
                           .Include(vp => vp.Producto)
                           .ToList();
        }

        // GET api/ventaproductos/5/3
        [HttpGet, Route("{idVenta:int}/{idProducto:int}", Name = "GetVentaProductoById")]
        public IHttpActionResult Get(int idVenta, int idProducto)
        {
            var vp = _context.VentaProductos
                             .Include(x => x.Venta)
                             .Include(x => x.Producto)
                             .FirstOrDefault(x => x.IdVenta == idVenta && x.IdProducto == idProducto);
            if (vp == null)
                return NotFound();
            return Ok(vp);
        }

        // POST api/ventaproductos
        [HttpPost, Route("")]
        public IHttpActionResult Post(VentaProducto ventaProducto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.VentaProductos.Add(ventaProducto);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetVentaProductoById",
                new { idVenta = ventaProducto.IdVenta, idProducto = ventaProducto.IdProducto },
                ventaProducto
            );
        }

        // PUT api/ventaproductos/5/3
        [HttpPut, Route("{idVenta:int}/{idProducto:int}")]
        public IHttpActionResult Put(int idVenta, int idProducto, VentaProducto ventaProducto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fuerzo las claves a las de la ruta
            ventaProducto.IdVenta = idVenta;
            ventaProducto.IdProducto = idProducto;

            var existing = _context.VentaProductos.Find(idVenta, idProducto);
            if (existing == null)
                return NotFound();

            existing.Cantidad = ventaProducto.Cantidad;
            existing.Subtotal = ventaProducto.Subtotal;
            _context.SaveChanges();

            return Ok(existing);
        }

        // DELETE api/ventaproductos/5/3
        [HttpDelete, Route("{idVenta:int}/{idProducto:int}")]
        public IHttpActionResult Delete(int idVenta, int idProducto)
        {
            var vp = _context.VentaProductos.Find(idVenta, idProducto);
            if (vp == null)
                return NotFound();

            _context.VentaProductos.Remove(vp);
            _context.SaveChanges();
            return Ok();
        }
    }
}