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

        [Required(ErrorMessage = "El estado de pago es obligatorio.")]
        [StringLength(20, ErrorMessage = "El estado de pago no puede exceder 20 caracteres.")]
        public string EstadoPago { get; set; } = "Pendiente"; // PENDIENTE, PAGADO, CANCELADO

        public DateTime? FechaPago { get; set; }

        // Propiedades de navegación (opcional)
        public virtual Venta Venta { get; set; }
        public virtual Reparacion Reparacion { get; set; }
    }
}