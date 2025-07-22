using API_RESTful.Data;
using API_RESTful.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_RESTful.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        private readonly TecnomovilContext _context;
        public ProductosController()
        {
            _context = new TecnomovilContext();

        }


        // GET api/<controller>
        [HttpGet]
        [Route("")]
        public IEnumerable<Producto> Get()
        {
            return _context.Productos.ToList();
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
                return NotFound();
            return Ok(producto);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Productos.Add(producto);
                _context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { controller = "productos", id = producto.IdProducto }, producto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Producto producto)
        {
            if (!ModelState.IsValid || id != producto.IdProducto)
                return BadRequest(ModelState);

            var existingProducto = _context.Productos.Find(id);
            if (existingProducto == null)
                return NotFound();

            existingProducto.Nombre = producto.Nombre;
            existingProducto.Precio = producto.Precio;
            _context.SaveChanges();
            return Ok(existingProducto);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return Ok();
        }

        //GET api/<controller>/search?nombre=
        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search(string nombre)
        {
            var productos = _context.Productos
                .Where(p => p.Nombre.Contains(nombre ?? ""))
                .ToList();
            return Ok(productos);
        }
    }
}