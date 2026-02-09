using System.Text;
using System.Data;
using MedApp.Domain.Models;
using MedApp.Application.DTOs;
using MedApp.Domain.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MedApp.Application.Extension.Mapping;
using MedApp.Application.Interfaces.Repositories;
using MedApp.Application.Interfaces.IServices;

namespace MedApp.Application.Services
{
    public class PacienteService : IPacienteServices
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Paciente> _logger;
        public PacienteService(IPacienteRepository repository, ILogger<Paciente> logger, IConfiguration configuration)
        {
            _pacienteRepository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> CrearPacienteAsync(PacienteDTO pacienteDto)
        {
            _ = new OperationResult();
            OperationResult result;
            try
            {
                _logger.LogInformation("Iniciando proceso de creacion de paciente");
                var existe = await _pacienteRepository.ExistePorCedulaAsync(pacienteDto.Cedula);
                if (existe)
                {
                    return OperationResult.Failure("El paciente ya existe");
                }

                var nuevoPaciente = PacienteMapper.MapToEntityCreate(pacienteDto);
                result = await _pacienteRepository.CrearPacienteAsync(nuevoPaciente);

                _logger.LogInformation("Paciente creado exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear paciente: {ex.Message}");
                result = OperationResult.Failure($"Error al crear paciente: {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult> ActualizarPacienteAsync(PacienteDTO pacienteDto)
        {
            _ = new OperationResult();
            OperationResult result;
            try
            {
                _logger.LogInformation("Iniciando proceso de actualizacion de paciente");
                var existe = await _pacienteRepository.ExistePorCedulaAsync(pacienteDto.Cedula);
                if (existe)
                {
                    return OperationResult.Success("El paciente existe");
                }

                var nuevoPaciente = PacienteMapper.MapToEntityCreate(pacienteDto);
                result = await _pacienteRepository.CrearPacienteAsync(nuevoPaciente);

                _logger.LogInformation("Paciente creado exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear paciente: {ex.Message}");
                result = OperationResult.Failure($"Error al crear paciente: {ex.Message}");
            }
            return result;
        }
        public async Task<OperationResult> ObtenerPorCedulaAsync(string cedula)
        {
            _ = new OperationResult();
            OperationResult result;
            try
            {
                _logger.LogInformation("Iniciando proceso de busqueda de paciente");
                var existe = await _pacienteRepository.ExistePorCedulaAsync(cedula);
                if (!existe)
                {
                    return OperationResult.Failure("El paciente no existe");
                }

                result = await _pacienteRepository.ObtenerPorCedulaAsync(cedula);
                if (result.IsSuccess)
                {
                    result = OperationResult.Success("Paciente encontrado", result.Data);
                    _logger.LogInformation("Paciente encontrado exitosamente");
                }
                else
                {
                    _logger.LogWarning("Paciente no encontrado");
                    return OperationResult.Failure("Paciente no encontrado");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar paciente: {ex.Message}");
                result = OperationResult.Failure($"Error al buscar paciente: {ex.Message}");
            }
            return result;  
        }

        public async Task<OperationResult> ObtenerPorNombreAsync(string nombre)
        {
            _ = new OperationResult();
            try
            {
                _logger.LogInformation("Iniciando proceso de busqueda de paciente por nombre");
                var pacientes = await _pacienteRepository.ObtenerPorNombreAsync(nombre);
                if (pacientes == null || !pacientes.Any())
                {
                    return OperationResult.Failure("No se encontraron pacientes con ese nombre");
                }
                return OperationResult.Success("Pacientes encontrados", pacientes);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error al buscar pacientes por nombre: {ex.Message}");
                return OperationResult.Failure($"Error al buscar pacientes por nombre: {ex.Message}");
            }
        }
    }
}
