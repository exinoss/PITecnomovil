using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Modelo
{
    public class VentaReparacion
    {
        public int IdVenta { get; set; }
        public int IdReparacion { get; set; }
        public decimal Subtotal { get; set; }

        public bool IsValid()
        {
            return IdVenta > 0 && IdReparacion > 0 && Subtotal > 0;
        }
    }
}
