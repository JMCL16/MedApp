using MedApp.DTOs;
using MedApp.Presentation.DTOs.Paciente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedApp.Presentation.Services 
{
    public class PacienteService
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _endpoint;

        public PacienteService()
        {
            string baseUrl = AppConfig.ApiUrl;
            _endpoint = $"{baseUrl}/Paciente";
        }

        public async Task<ApiResponse<PacienteDTO>> CrearPaciente(PacienteDTO pacienteDto)
        {
            var json = JsonConvert.SerializeObject(pacienteDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_endpoint, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ApiResponse<PacienteDTO>>(jsonResponse);
            return resultado; 
        }

        public async Task<PacienteDTO> ObtenerPacientePorCedula(string cedula)
        {
            try
            {
                var response = await _client.GetAsync($"{_endpoint}?cedula={cedula}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<ApiResponse<PacienteDTO>>(jsonResponse);
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
                    Console.WriteLine($"Error al obtener paciente: {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener paciente: {ex.Message}");
                return null;
            }
        }
        public async Task<List<PacienteDTO>> BuscarPorNombre(string nombre)
        {
            try
            {
                var response = await _client.GetAsync($"{_endpoint}/buscar/{nombre}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<ApiResponse<List<PacienteDTO>>>(jsonResponse);
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
                    Console.WriteLine($"Error al buscar paciente por nombre: {response.ReasonPhrase}");
                    return new List<PacienteDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar paciente por nombre: {ex.Message}");
                return null;
            }
        }
    }
}
