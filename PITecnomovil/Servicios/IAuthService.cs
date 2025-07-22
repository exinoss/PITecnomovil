using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    internal interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
