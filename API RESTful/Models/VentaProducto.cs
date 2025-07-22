using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_RESTful.Models
{
    public class VentaProducto
    {
        [Key, Column(Order = 0)]
        public int IdVenta { get; set; }

        [Key, Column(Order = 1)]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        public decimal Subtotal { get; set; }

        public virtual Venta Venta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}