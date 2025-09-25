using Newtonsoft.Json;
using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    public class FacturaService : IFacturaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:44396/api/facturas";

        public FacturaService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Factura>> GetFacturasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseUrl);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Factura>>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener facturas: {ex.Message}");
            }
        }

        public async Task<Factura> GetFacturaByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Factura>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener factura: {ex.Message}");
            }
        }

        public async Task AddFacturaAsync(Factura factura)
        {
            try
            {
                var json = JsonConvert.SerializeObject(factura, new JsonSerializerSettings
                {
                    Culture = CultureInfo.InvariantCulture
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión con la API: {ex.Message}. Verifique que la API esté ejecutándose en {_baseUrl}");
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al conectar con la API: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar factura: {ex.Message}");
            }
        }

        public async Task UpdateFacturaAsync(Factura factura)
        {
            try
            {
                var json = JsonConvert.SerializeObject(factura, new JsonSerializerSettings
                {
                    Culture = CultureInfo.InvariantCulture
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/{factura.IdFactura}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar factura: {ex.Message}");
            }
        }

        public async Task DeleteFacturaAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar factura: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}