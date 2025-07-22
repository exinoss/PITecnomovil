using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PITecnomovil.Modelo;

namespace PITecnomovil.Servicios
{
    public interface IProductoService
    {
        Task<List<Producto>> GetProductosAsync();
        Task AddProductoAsync(Producto producto);
        Task<Producto> GetProductoAsync(int id);
        Task UpdateProductoAsync(int id, Producto producto);
        Task DeleteProductoAsync(int id);
        Task<List<Producto>> SearchProductosAsync(string nombre);
    }
}
