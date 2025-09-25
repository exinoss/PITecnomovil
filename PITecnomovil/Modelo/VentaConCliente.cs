using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Modelo
{
    public class VentaConCliente
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public string NombreCliente { get; set; }
        public string CedulaCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string ContactoCliente { get; set; }
    }
}