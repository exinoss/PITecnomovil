using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Modelo
{
    public class VentaProducto
    {
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }

        public bool IsValid()
        {
            return IdVenta > 0 && IdProducto > 0 && Cantidad > 0 && Subtotal > 0;
        }
    }
}
