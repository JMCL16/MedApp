using Microsoft.AspNetCore.Mvc;
using MedApp.Domain.Models;
using MedApp.Application.Interfaces.IServices;
using MedApp.Application.DTOs.Paciente;

namespace MedApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteServices _pacienteService;
        private readonly ILogger<PacienteController> _logger;
        public PacienteController(IPacienteServices pacienteService, ILogger<PacienteController> logger)
        {
            _pacienteService = pacienteService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPaciente([FromBody] PacienteDTO pacientedto)
        {

            var result = await _pacienteService.CrearPacienteAsync(pacientedto);
            if (result.IsSuccess)
            {
                _logger.LogInformation("El paciente ha sido creado");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Ha ocurrido un error al crear el paciente");
                return BadRequest(result);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPacientePorCedula(string cedula)
        {
            var result = await _pacienteService.ObtenerPorCedulaAsync(cedula);
            if (result.IsSuccess)
            {
                _logger.LogInformation("El paciente ha sido encontrado");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("El paciente no ha sido recuperado");
                return BadRequest(result);
            }
        }

        [HttpGet("buscar/{nombre}")]
        public async Task<IActionResult> ObtenerPacientesPorNombre(string nombre)
        {
            var result = await _pacienteService.ObtenerPorNombreAsync(nombre);
            if (result.IsSuccess)
            {
                _logger.LogInformation("El paciente ha sido encontrado");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("El paciente no ha sido recuperado");
                return BadRequest(result);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ActualizarPaciente([FromBody] PacienteUpdateDTO pacienteUpdateDto)
        {
            var result = await _pacienteService.ActualizarPacienteAsync(pacienteUpdateDto);
            if (result.IsSuccess)
            {
                _logger.LogInformation("El paciente ha sido actualizado");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Ha ocurrido un error al actualizar el paciente");
                return BadRequest(result);
            }
        }
    }
}
