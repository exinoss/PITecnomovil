using PITecnomovil.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    public interface IFacturaService
    {
        Task<IEnumerable<Factura>> GetFacturasAsync();
        Task<Factura> GetFacturaByIdAsync(int id);
        Task<Factura> AddFacturaAsync(Factura factura);
        Task UpdateFacturaAsync(Factura factura);
        Task DeleteFacturaAsync(int id);
        
        // Métodos SQL directos para evitar problemas con la API
        bool InsertFacturaSQL(Factura factura, out int idFactura);
        bool InsertPagoSQL(Pago pago);
    }
}