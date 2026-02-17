using MedApp.Application.DTOs.Consultas;
using MedApp.Application.Extension.Validators.ConsultaValidators;
using MedApp.Application.Interfaces.Repositories;
using MedApp.Domain.Base;
using MedApp.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;


namespace MedApp.Persistence.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _conexionBD;
        private readonly ILogger _logger;
        private readonly ConsultaValidator _consultaValidator;
        private readonly ConsultaUpdateValidator _consultaUpdateValidator;
        public ConsultaRepository(IConfiguration configuration, ILogger<Consulta> logger, ConsultaValidator consultaValidator, ConsultaUpdateValidator consultaUpdateValidator) 
        {
            _configuration = configuration;
            _conexionBD = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found");
            _logger = logger;
            _consultaValidator = consultaValidator;
            _consultaUpdateValidator = consultaUpdateValidator;
        }
        public async Task<OperationResult> ActualizarConsultaAsync(Consulta consulta)
        {
            _ = new OperationResult();
            OperationResult result;

            try
            {
                _logger.LogInformation("Iniciando validación de la consulta");
                var validationResult = await _consultaUpdateValidator.ValidateAsync(consulta);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("El siguiente campo no es valido {Errors}", validationResult.Errors);
                    return OperationResult.Failure($"El siguiente campo no es valido {validationResult}");
                }

                using (var conn = new SqlConnection(_conexionBD))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand("sp_ActualizarConsulta", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdConsulta", consulta.IdConsulta);
                        cmd.Parameters.AddWithValue("@Cedula", consulta.CedulaPaciente ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaConsulta", consulta.FechaConsulta);
                        cmd.Parameters.AddWithValue("@Diagnostico", consulta.Diagnostico ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tratamiento", consulta.Tratamiento ?? (object)DBNull.Value);
                        object? rowsAffected = await cmd.ExecuteScalarAsync();
                        if (Convert.ToInt32(rowsAffected) > 0)
                        {
                            _logger.LogInformation("Consulta actualizada exitosamente");
                            result = OperationResult.Success("Consulta actualizada exitosamente");
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo actualizar la consulta");
                            result = OperationResult.Failure("No se pudo actualizar la consulta");
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la consulta");
                result = OperationResult.Failure($"Error al actualizar la consulta {ex}");
            }
            return result;
        }

        public async Task<OperationResult> CrearConsultaAsync(Consulta consulta)
        {
            _ = new OperationResult();
            OperationResult result;

            try
            {
                _logger.LogInformation("Iniciando validación de la consulta");
                var validationResult = await _consultaValidator.ValidateAsync(consulta);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("El siguiente campo no es valido {Errors}", validationResult.Errors);
                    return OperationResult.Failure($"El siguiente campo no es valido {validationResult}");
                }

                using (var conn = new SqlConnection(_conexionBD))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand("sp_CrearConsulta", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Cedula", consulta.CedulaPaciente ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaConsulta", consulta.FechaConsulta);
                        cmd.Parameters.AddWithValue("@Diagnostico", consulta.Diagnostico ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tratamiento", consulta.Tratamiento ?? (object)DBNull.Value);
                        object? rowsAffected = await cmd.ExecuteScalarAsync();
                        int newNum = Convert.ToInt32(rowsAffected);
                        if (newNum > 0)
                        {
                            _logger.LogInformation("Consulta creada exitosamente");
                            result = OperationResult.Success("Consulta creada exitosamente");
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo crear la consulta");
                            result = OperationResult.Failure("No se pudo crear la consulta");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la consulta");
                result = OperationResult.Failure("Error al crear la consulta");
            }
            return result;
        }

        // Hacer lista para obtener consulta
        public async Task<IEnumerable<ConsultaDTO>> ObtenerPorCedulaAsync(string cedula)
        {
            _ = new OperationResult();
            OperationResult result;
            List<ConsultaDTO> historial = new List<ConsultaDTO>();
            try
            {
                _logger.LogInformation("Obteniendo consulta por cédula");
                using (var conn = new SqlConnection(_conexionBD))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_ObtenerConsultasPorCedula", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Cedula", cedula);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                historial.Add(new ConsultaDTO
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("IdConsulta")),
                                    FechaConsulta = reader.GetDateTime(reader.GetOrdinal("FechaConsulta")),
                                    Diagnostico = reader.IsDBNull(reader.GetOrdinal("Diagnostico")) ? null : reader.GetString(reader.GetOrdinal("Diagnostico")),
                                    Tratamiento = reader.IsDBNull(reader.GetOrdinal("Tratamiento")) ? null : reader.GetString(reader.GetOrdinal("Tratamiento"))

                                }
                                );
                            }
                            ;
                            if (historial.Count > 0)
                            {
                                _logger.LogInformation("Consulta obtenida exitosamente");
                                result = OperationResult.Success("Consulta obtenida exitosamente", historial);
                            }
                            else
                            {
                                _logger.LogWarning("No se encontraron consultas para la cédula proporcionada");
                                result = OperationResult.Failure("No se encontraron consultas para la cédula proporcionada");

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la consulta por cedula: {cedula}", cedula);
                result = OperationResult.Failure(ex.Message);
            }
            return historial;
        }

        public Task<IEnumerable<Consulta>> ObtenerPorPacienteIdAsync(int pacienteId)
        {
            throw new NotImplementedException();
        }

    }
}
