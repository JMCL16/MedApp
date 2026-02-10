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
        public ConsultaRepository(IConfiguration configuration, ILogger<Consulta> logger, ConsultaValidator consultaValidator) 
        {
            _configuration = configuration;
            _conexionBD = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found");
            _logger = logger;
            _consultaValidator = consultaValidator;
        }
        public Task<OperationResult> ActualizarConsultaAsync(Consulta consulta)
        {
            throw new NotImplementedException();
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

                var presult = await ExecuteStoredProcedureAsync("sp_CrearConsulta",
                    new SqlParameter("@FechaConsulta", consulta.FechaConsulta),
                    new SqlParameter("@Diagnostico", consulta.Diagnostico ?? (object)DBNull.Value),
                    new SqlParameter("@Tratamiento", consulta.Tratamiento ?? (object)DBNull.Value)
                );

                if (presult > 0)
                {
                    result = OperationResult.Success("Consulta creada exitosamente");
                }
                else
                {
                    result = OperationResult.Failure("No se pudo crear la consulta");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la consulta");
                result = OperationResult.Failure("Error al crear la consulta");
            }
            return result;
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Consulta>> ObtenerPorPacienteIdAsync(int pacienteId)
        {
            throw new NotImplementedException();
        }

        private async Task<int> ExecuteStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters)
        {

            using (var conn = new SqlConnection(_conexionBD))
            {
                using (var cmd = new SqlCommand(storedProcedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
