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
    public class PagoService : IPagoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:44396/api/pagos";

        public PagoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Pago>> GetPagosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseUrl);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Pago>>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener pagos: {ex.Message}");
            }
        }

        public async Task<Pago> GetPagoByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pago>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener pago: {ex.Message}");
            }
        }

        public async Task<Pago> AddPagoAsync(Pago pago)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pago, new JsonSerializerSettings
                {
                    Culture = CultureInfo.InvariantCulture
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl, content);
                response.EnsureSuccessStatusCode();
                
                // Obtener el pago creado desde la respuesta
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pago>(responseJson);
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
                throw new Exception($"Error al agregar pago: {ex.Message}");
            }
        }

        public async Task UpdatePagoAsync(Pago pago)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pago, new JsonSerializerSettings
                {
                    Culture = CultureInfo.InvariantCulture
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/{pago.IdPago}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar pago: {ex.Message}");
            }
        }

        public async Task DeletePagoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar pago: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}