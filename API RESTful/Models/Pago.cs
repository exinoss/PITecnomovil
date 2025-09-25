using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_RESTful.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [StringLength(20, ErrorMessage = "El método de pago no puede exceder 20 caracteres.")]
        public string Metodo { get; set; }

        [Required(ErrorMessage = "El ID de factura es obligatorio.")]
        public int IdFactura { get; set; }

        // Navigation property
        public virtual Factura Factura { get; set; }
    }
}