using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_RESTful.Models
{
    public class VentaReparacion
    {
        [Key, Column(Order = 0)]
        public int IdVenta { get; set; }

        [Key, Column(Order = 1)]
        public int IdReparacion { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        public decimal Subtotal { get; set; }

        // Propiedades de navegación (opcional)
        public virtual Venta Venta { get; set; }
        public virtual Reparacion Reparacion { get; set; }
    }
}