using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Modelo
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Cedula { get; set; }
        public string Contacto { get; set; }

        //Validaciones para logica de negocio
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Nombres) && !string.IsNullOrEmpty(Cedula);
        }
    }
}
