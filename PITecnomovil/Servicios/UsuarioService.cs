using Newtonsoft.Json;
using PITecnomovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PITecnomovil.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44349/")
            };
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            var resp = await _httpClient.GetAsync("api/usuarios");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Usuario>>(json);
        }

        public async Task<Usuario> GetUsuarioAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"api/usuarios/{id}");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Usuario>(json);
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(usuario),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PostAsync("api/usuarios", content);
            if (resp.StatusCode == HttpStatusCode.Conflict)
            {
                var error = await resp.Content.ReadAsStringAsync();
                throw new InvalidOperationException(error);
            }
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateUsuarioAsync(int id, Usuario usuario)
        {
            usuario.IdUsuario = id;
            var content = new StringContent(
                JsonConvert.SerializeObject(usuario),
                Encoding.UTF8,
                "application/json"
            );
            var resp = await _httpClient.PutAsync($"api/usuarios/{id}", content);
            if (resp.StatusCode == HttpStatusCode.Conflict)
            {
                var error = await resp.Content.ReadAsStringAsync();
                throw new InvalidOperationException(error);
            }

            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var resp = await _httpClient.DeleteAsync($"api/usuarios/{id}");
            resp.EnsureSuccessStatusCode();
        }
    }
}
