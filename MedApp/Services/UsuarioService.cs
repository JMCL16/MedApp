using MedApp.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MedApp.DTOs;

namespace MedApp.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _client;
        private readonly string _endpoint;

        public UsuarioService()
        {
            string baseUrl = AppConfig.ApiUrl;
            _client = new HttpClient();
            _endpoint = $"{baseUrl}/Usuario";
        }

        public async Task<List<UsuarioDTO>> ObtenerTodos()
        {
            var response = await _client.GetAsync($"{_endpoint}/Usuario");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ApiResponse<List<UsuarioDTO>>>(content);
                if (resultado != null && resultado.IsSuccess && resultado.Data != null)
                {
                    return resultado.Data;
                }
            }
            return new List<UsuarioDTO>();
        }

        public async Task<bool> CrearUsuario(UsuarioDTO usuarioDto)
        {
            var json = JsonConvert.SerializeObject(usuarioDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync($"{_endpoint}/Usuario", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear usuario: {ex.Message}");
                return false;
            }
        }
    }
}
