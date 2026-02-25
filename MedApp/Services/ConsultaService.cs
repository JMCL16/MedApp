using MedApp.Presentation.DTOs.Consultas;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedApp.DTOs;
using Newtonsoft.Json;

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
            var json = JsonConvert.SerializeObject(consultaDto);
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

        public async Task<List<ConsultaDTO>> ObtenerConsultaPorCedula(string cedula)
        {
            try
            {
                var response = await _client.GetAsync($"{_endpoint}?cedula={cedula}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<ApiResponse<List<ConsultaDTO>>>(jsonResponse);
                    if (resultado != null && resultado.IsSuccess)
                    {
                        return resultado.Data;
                    }
                    else
                    {
                        Console.WriteLine($"Error en la respuesta del API: {resultado?.Message}");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine($"Error al obtener consulta: {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener consulta: {ex.Message}");
                return null;
            }
        }
    }
}
