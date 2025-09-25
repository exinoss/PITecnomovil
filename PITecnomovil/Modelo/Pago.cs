using System;
using System.ComponentModel.DataAnnotations;

namespace PITecnomovil.Modelo
{
    public class Pago
    {
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

        public bool IsValid()
        {
            return Fecha != default(DateTime) &&
                   Monto > 0 &&
                   !string.IsNullOrWhiteSpace(Metodo) &&
                   IdFactura > 0;
        }
    }
}