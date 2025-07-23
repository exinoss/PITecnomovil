using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    internal interface IVentaProductoService
    {
        Task<List<VentaProducto>> GetVentaProductosAsync();
        Task<VentaProducto> GetVentaProductoAsync(int idVenta, int idProducto);
        Task AddVentaProductoAsync(VentaProducto ventaProducto);
        Task UpdateVentaProductoAsync(int idVenta, int idProducto, VentaProducto ventaProducto);
        Task DeleteVentaProductoAsync(int idVenta, int idProducto);
    }
}
