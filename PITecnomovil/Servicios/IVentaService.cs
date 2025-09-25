using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    internal interface IVentaService
    {
        Task<List<Venta>> GetVentasAsync();
        Task<Venta> GetVentaAsync(int id);
        Task AddVentaAsync(Venta venta);
        Task UpdateVentaAsync(int id, Venta venta);
        Task DeleteVentaAsync(int id);
    }
}
