using MedApp.Presentation.DTOs.Consultas;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MedApp.Services
{
    public class ConsultaService
    {
        private static HttpClient _client = new HttpClient();
        private readonly string _endpoint;

        public ConsultaService()
        {
            string apiBaseUrl = AppConfig.ApiUrl;
            _endpoint = $"{apiBaseUrl}/Consulta";
        }

        public async Task<bool> CrearConsulta(ConsultaDTO consultaDto)
        {
            var json = JsonSerializer.Serialize(consultaDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync(_endpoint, content);
                return response.IsSuccessStatusCode;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear consulta: {ex.Message}");
                return false;
            }
        }
    }
}
