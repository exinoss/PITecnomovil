using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PITecnomovil.Modelo
{
    public class Factura
    {
        public int IdFactura { get; set; }

        [Required(ErrorMessage = "El número de factura es obligatorio.")]
        [StringLength(30, ErrorMessage = "El número de factura no puede exceder 30 caracteres.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "La fecha de emisión es obligatoria.")]
        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        public decimal Subtotal { get; set; }

        [Required(ErrorMessage = "El IVA es obligatorio.")]
        public decimal IVA { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(20, ErrorMessage = "El estado no puede exceder 20 caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El ID de venta es obligatorio.")]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "El ID de cliente es obligatorio.")]
        public int IdCliente { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Numero) &&
                   FechaEmision != default(DateTime) &&
                   Subtotal > 0 &&
                   IVA >= 0 &&
                   Total > 0 &&
                   !string.IsNullOrWhiteSpace(Estado) &&
                   IdVenta > 0 &&
                   IdCliente > 0;
        }
    }
}