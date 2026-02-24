using MedApp.DTOs.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedApp.Services
{
    public class AuthService
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _endpoint = $"{AppConfig.ApiUrl}/Autentication";

        public async Task<AuthResponseDTO> LoginAsync(string username, string password)
        {
            var loginDTO = new LoginDTO
            {
                UserName = username,
                Password = password
            };
            var json = JsonConvert.SerializeObject(loginDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync($"{_endpoint}/login", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AuthResponseDTO>(responseContent);
                }
                else
                {
                    throw new Exception("Error al iniciar sesión.");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión: {ex.Message}");
            }

        }
    }
}
