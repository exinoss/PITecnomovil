using Newtonsoft.Json;
using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private readonly string _connectionString = "Server=.;Database=TecnomovilDB;User ID=sa;Password=123456;";

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

        public async Task<Factura> AddFacturaAsync(Factura factura)
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
                
                // Obtener la factura creada desde la respuesta
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Factura>(responseJson);
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

        // Métodos SQL directos para evitar problemas con la API
        public bool InsertFacturaSQL(Factura factura, out int idFactura)
        {
            idFactura = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    string query = @"
                        INSERT INTO Factura (Numero, FechaEmision, Subtotal, IVA, Total, Estado, IdVenta, IdCliente)
                        VALUES (@Numero, @FechaEmision, @Subtotal, @IVA, @Total, @Estado, @IdVenta, @IdCliente);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Numero", factura.Numero);
                        command.Parameters.AddWithValue("@FechaEmision", factura.FechaEmision);
                        command.Parameters.AddWithValue("@Subtotal", factura.Subtotal);
                        command.Parameters.AddWithValue("@IVA", factura.IVA);
                        command.Parameters.AddWithValue("@Total", factura.Total);
                        command.Parameters.AddWithValue("@Estado", factura.Estado);
                        command.Parameters.AddWithValue("@IdVenta", factura.IdVenta);
                        command.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                        
                        var result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out idFactura))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log del error si es necesario
                System.Diagnostics.Debug.WriteLine($"Error al insertar factura: {ex.Message}");
            }
            
            return false;
        }
        
        public bool InsertPagoSQL(Pago pago)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    string query = @"
                        INSERT INTO Pago (Fecha, Monto, Metodo, IdFactura)
                        VALUES (@Fecha, @Monto, @Metodo, @IdFactura)";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Fecha", pago.Fecha);
                        command.Parameters.AddWithValue("@Monto", pago.Monto);
                        command.Parameters.AddWithValue("@Metodo", pago.Metodo);
                        command.Parameters.AddWithValue("@IdFactura", pago.IdFactura);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log del error si es necesario
                System.Diagnostics.Debug.WriteLine($"Error al insertar pago: {ex.Message}");
            }
            
            return false;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}