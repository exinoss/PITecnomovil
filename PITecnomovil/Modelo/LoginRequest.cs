using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Modelo
{
    internal class LoginRequest
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(NombreUsuario) && !string.IsNullOrEmpty(Clave);
        }
    }
}
