using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    internal interface IVentaReparacionService
    {
        Task<List<VentaReparacion>> GetVentaReparacionesAsync();
        Task<VentaReparacion> GetVentaReparacionAsync(int idVenta, int idReparacion);
        Task AddVentaReparacionAsync(VentaReparacion ventaReparacion);
        Task UpdateVentaReparacionAsync(int idVenta, int idReparacion, VentaReparacion ventaReparacion);
        Task DeleteVentaReparacionAsync(int idVenta, int idReparacion);
    }
}
