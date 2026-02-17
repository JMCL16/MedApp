using MedApp.Application.DTOs.Consultas;
using MedApp.Application.Extension.Mapping;
using MedApp.Application.Interfaces.IServices;
using MedApp.Application.Interfaces.Repositories;
using MedApp.Domain.Base;
using MedApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedApp.Application.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly ILogger<Consulta> _logger;
        private readonly IConfiguration _configuration;
        public ConsultaService(IConsultaRepository consultaRepository, ILogger<Consulta> logger, IConfiguration configuration)
        {
            _consultaRepository = consultaRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> ActualizarConsultaAsync(ConsultaUpdateDTO consultaUpdateDto)
        {
            _ = new OperationResult();
            OperationResult result;

            try
            {
                _logger.LogInformation("Inicio actualizacion consulta");
                var consultaActualizada = ConsultaMapper.MapToUpdateEntity(consultaUpdateDto);
                result = await _consultaRepository.ActualizarConsultaAsync(consultaActualizada);
                _logger.LogInformation("Consulta actualizada exitosamente");
            }
            catch
            {
                _logger.LogError("Error al actualizar la consulta.");
                result = OperationResult.Failure($"Error al actualizar la consulta.");
            }
            return result;
        }

        public async Task<OperationResult> CrearConsultaAsync(ConsultaDTO consultaDto)
        {
            _ = new OperationResult();
            OperationResult result;

            try
            {
                _logger.LogInformation("Inicio creacion consulta");
                var nuevaConsulta = ConsultaMapper.MapToCreateEntity(consultaDto);
                result = await _consultaRepository.CrearConsultaAsync(nuevaConsulta);
                _logger.LogInformation("Consulta creada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la consulta.");
                result = OperationResult.Failure($"Error al crear la consulta. {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult> ObtenerPorCedulaAsync(string cedula)
        {
            _ = new OperationResult();
            try
            {
                _logger.LogInformation("Inicio obtencion consulta por cedula");
                var consultas = await _consultaRepository.ObtenerPorCedulaAsync(cedula);
                if (consultas == null || !consultas.Any())
                {
                    _logger.LogWarning("No se encontraron consultas para la cédula proporcionada");
                    return OperationResult.Failure($"No se encontraron consultas para la cédula");
                }
                else
                {
                    return OperationResult.Success("Consulta obtenida exitosamente", consultas);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener consulta por cédula.");
                return OperationResult.Failure($"Error al obtener consulta por cédula. {ex.Message}"); 
            }
        }

        public Task<OperationResult> ObtenerPorPacienteIdAsync(int pacienteId)
        {
            throw new NotImplementedException();
        }
    }
}
