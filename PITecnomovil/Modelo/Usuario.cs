using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Modelo
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(NombreUsuario) && !string.IsNullOrEmpty(Clave) && !string.IsNullOrEmpty(Rol);
        }
    }
}
