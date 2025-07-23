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
    public class VentaService : IVentaService
    {
        private readonly HttpClient _httpClient;

        public VentaService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44349/")
            };
        }

        public async Task<List<Venta>> GetVentasAsync()
        {
            var resp = await _httpClient.GetAsync("api/ventas");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Venta>>(json);
        }

        public async Task<Venta> GetVentaAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"api/ventas/{id}");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Venta>(json);
        }

        public async Task AddVentaAsync(Venta venta)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(venta),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PostAsync("api/ventas", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateVentaAsync(int id, Venta venta)
        {
            venta.IdVenta = id;
            var content = new StringContent(
                JsonConvert.SerializeObject(venta),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PutAsync($"api/ventas/{id}", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteVentaAsync(int id)
        {
            var resp = await _httpClient.DeleteAsync($"api/ventas/{id}");
            resp.EnsureSuccessStatusCode();
        }
    }
}
