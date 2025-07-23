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
    public class VentaProductoService : IVentaProductoService
    {
        private readonly HttpClient _httpClient;

        public VentaProductoService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44349/")
            };
        }

        public async Task<List<VentaProducto>> GetVentaProductosAsync()
        {
            var resp = await _httpClient.GetAsync("api/ventaproductos");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<VentaProducto>>(json);
        }

        public async Task<VentaProducto> GetVentaProductoAsync(int idVenta, int idProducto)
        {
            var resp = await _httpClient.GetAsync($"api/ventaproductos/{idVenta}/{idProducto}");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VentaProducto>(json);
        }

        public async Task AddVentaProductoAsync(VentaProducto ventaProducto)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(ventaProducto),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PostAsync("api/ventaproductos", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateVentaProductoAsync(int idVenta, int idProducto, VentaProducto ventaProducto)
        {
            // Asegura que el objeto lleva las claves correctas
            ventaProducto.IdVenta = idVenta;
            ventaProducto.IdProducto = idProducto;

            var content = new StringContent(
                JsonConvert.SerializeObject(ventaProducto),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PutAsync($"api/ventaproductos/{idVenta}/{idProducto}", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteVentaProductoAsync(int idVenta, int idProducto)
        {
            var resp = await _httpClient.DeleteAsync($"api/ventaproductos/{idVenta}/{idProducto}");
            resp.EnsureSuccessStatusCode();
        }
    }
}
