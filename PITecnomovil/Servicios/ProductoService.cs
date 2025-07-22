using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PITecnomovil.Modelo;

namespace PITecnomovil.Servicios
{
    
    internal class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44349/"); 
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            var response = await _httpClient.GetAsync("api/productos");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(json);
        }
        public async Task AddProductoAsync(Producto producto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/productos", content);
            response.EnsureSuccessStatusCode();
            
        }
        public async Task<Producto> GetProductoAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/productos/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Producto>(json);
        }

        public async Task UpdateProductoAsync(int id, Producto producto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(producto), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/productos/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Producto>> SearchProductosAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"api/productos/search?nombre={Uri.EscapeDataString(nombre ?? "")}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(json);
        }
    }
}
