﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Modelo
{
    internal class LoginResponse
    {
        public int IdUsuario { get; set; }
        public string Message { get; set; }
        public string Rol { get; set; }
    }
}
