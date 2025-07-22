using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PITecnomovil.Modelo;

namespace PITecnomovil.Servicios
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetClientesAsync();
        Task AddClienteAsync(Cliente cliente);
        Task<Cliente> GetClienteAsync(int id);
        Task UpdateClienteAsync(int id, Cliente cliente);
        Task DeleteClienteAsync(int id);
    }
}
