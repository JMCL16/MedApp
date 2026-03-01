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
        private readonly HttpClient _client = new HttpClient();
        private readonly string _endpoint = $"{AppConfig.ApiUrl}/Autentication";

        public async Task<List<UsuarioDTO>> ObtenerTodos()
        {
            var response = await _client.GetAsync($"{_endpoint}");
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

        public async Task<ApiResponse<CrearUsuarioDTO>> CrearUsuario(CrearUsuarioDTO crearUsuarioDto)
        {
            var json = JsonConvert.SerializeObject(crearUsuarioDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_endpoint}/register", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ApiResponse<CrearUsuarioDTO>>(jsonResponse);
            return resultado;
        }

        public async Task<ApiResponse<UsuarioDTO>> ActualizarRol(ActualizarRolDTO actualizarRolDTO)
        {
            var json = JsonConvert.SerializeObject(actualizarRolDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_endpoint}", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ApiResponse<UsuarioDTO>>(jsonResponse);
            return resultado ?? new ApiResponse<UsuarioDTO> { IsSuccess = false, Message = "Error al actualizar rol" };
        }
    }
}
