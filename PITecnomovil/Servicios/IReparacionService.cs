using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    internal interface IReparacionService
    {
        Task<List<Reparacion>> GetReparacionesAsync();
        Task<Reparacion> GetReparacionAsync(int id);
        Task AddReparacionAsync(Reparacion reparacion);
        Task UpdateReparacionAsync(int id, Reparacion reparacion);
        Task DeleteReparacionAsync(int id);
    }
}
