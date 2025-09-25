using Newtonsoft.Json;
using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    public class ReparacionService : IReparacionService
    {
        private readonly HttpClient _httpClient;

        public ReparacionService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44349/")
            };
        }

        public async Task<List<Reparacion>> GetReparacionesAsync()
        {
            var resp = await _httpClient.GetAsync("api/reparaciones");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Reparacion>>(json);
        }

        public async Task<Reparacion> GetReparacionAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"api/reparaciones/{id}");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Reparacion>(json);
        }

        public async Task AddReparacionAsync(Reparacion reparacion)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(reparacion),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PostAsync("api/reparaciones", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateReparacionAsync(int id, Reparacion reparacion)
        {
            reparacion.IdReparacion = id;
            var content = new StringContent(
                JsonConvert.SerializeObject(reparacion),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PutAsync($"api/reparaciones/{id}", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteReparacionAsync(int id)
        {
            var resp = await _httpClient.DeleteAsync($"api/reparaciones/{id}");
            resp.EnsureSuccessStatusCode();
        }

        public async Task<List<Reparacion>> GetUnpaidRepairsByClientAsync(int idCliente)
        {
            var resp = await _httpClient.GetAsync($"api/reparaciones/cliente/{idCliente}/unpaid");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Reparacion>>(json);
        }
    }
}
