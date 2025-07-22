using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_RESTful.Models
{
    public class Reparacion
    {
        [Key]
        public int IdReparacion { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        public DateTime FechaIngreso { get; set; }

        public DateTime? FechaEntrega { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(20, ErrorMessage = "El estado no puede exceder 20 caracteres.")]
        public string Estado { get; set; }

        public string Diagnostico { get; set; }

        public string Observaciones { get; set; }

        [Required(ErrorMessage = "El dispositivo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El dispositivo no puede exceder 50 caracteres.")]
        public string Dispositivo { get; set; }

        [Required(ErrorMessage = "El precio del servicio es obligatorio.")]
        public decimal PrecioServicio { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<VentaReparacion> VentaReparaciones { get; set; }

    }
}