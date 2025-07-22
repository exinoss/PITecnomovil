using Newtonsoft.Json;
using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    internal class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44349/") 
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/login", content);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return null;

            response.EnsureSuccessStatusCode();

            var respJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginResponse>(respJson);
        }
    }
}
