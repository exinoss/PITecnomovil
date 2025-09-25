using PITecnomovil.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    public interface IPagoService
    {
        Task<IEnumerable<Pago>> GetPagosAsync();
        Task<Pago> GetPagoByIdAsync(int id);
        Task<Pago> AddPagoAsync(Pago pago);
        Task UpdatePagoAsync(Pago pago);
        Task DeletePagoAsync(int id);
    }
}