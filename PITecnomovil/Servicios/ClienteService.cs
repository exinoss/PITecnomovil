using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PITecnomovil.Modelo;
using System.Net;

namespace PITecnomovil.Servicios
{
    internal class ClienteService  : IClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44349/"); 
        }
        
        public async Task<List<Cliente>> GetClientesAsync()
        {
            var response = await _httpClient.GetAsync("api/clientes");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Cliente>>(json);
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/clientes", content);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException(error);
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cliente>(json);
        }

        public async Task UpdateClienteAsync(int id, Cliente cliente)
        {
            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/clientes/{id}", content);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException(error);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteClienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/clientes/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Cliente>> SearchProductosAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"api/clientes/search?nombre={Uri.EscapeDataString(nombre ?? "")}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Cliente>>(json);
        }
    }
}
