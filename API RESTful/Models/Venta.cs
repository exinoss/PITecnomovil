using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_RESTful.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public int IdUsuario { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<VentaReparacion> VentaReparaciones { get; set; }

    }
}