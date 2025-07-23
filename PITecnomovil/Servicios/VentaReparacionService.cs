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
    public class VentaReparacionService : IVentaReparacionService
    {
        private readonly HttpClient _httpClient;

        public VentaReparacionService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44349/")
            };
        }

        public async Task<List<VentaReparacion>> GetVentaReparacionesAsync()
        {
            var resp = await _httpClient.GetAsync("api/ventareparaciones");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<VentaReparacion>>(json);
        }

        public async Task<VentaReparacion> GetVentaReparacionAsync(int idVenta, int idReparacion)
        {
            var resp = await _httpClient.GetAsync($"api/ventareparaciones/{idVenta}/{idReparacion}");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VentaReparacion>(json);
        }

        public async Task AddVentaReparacionAsync(VentaReparacion ventaReparacion)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(ventaReparacion),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PostAsync("api/ventareparaciones", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateVentaReparacionAsync(int idVenta, int idReparacion, VentaReparacion ventaReparacion)
        {
            // Forzamos claves según la ruta
            ventaReparacion.IdVenta = idVenta;
            ventaReparacion.IdReparacion = idReparacion;

            var content = new StringContent(
                JsonConvert.SerializeObject(ventaReparacion),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PutAsync($"api/ventareparaciones/{idVenta}/{idReparacion}", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteVentaReparacionAsync(int idVenta, int idReparacion)
        {
            var resp = await _httpClient.DeleteAsync($"api/ventareparaciones/{idVenta}/{idReparacion}");
            resp.EnsureSuccessStatusCode();
        }
    }
}
