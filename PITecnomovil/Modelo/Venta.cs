using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Modelo
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }

        public bool IsValid()
        {
            return Fecha != default && Total > 0 && IdCliente > 0 && IdUsuario > 0;
        }
    }
}
