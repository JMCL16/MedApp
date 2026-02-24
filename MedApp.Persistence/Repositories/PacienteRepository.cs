using MedApp.Application.DTOs;
using MedApp.Application.Extension.Validators.PacienteValidators;
using MedApp.Application.Interfaces.Repositories;
using MedApp.Domain.Base;
using MedApp.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text.Json;


namespace MedApp.Persistence.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly string _conexionBD;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Paciente> _logger;


        public PacienteRepository(IConfiguration configuration, ILogger<Paciente> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _conexionBD = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string not fuond");
        }

        public async Task<OperationResult> ActualizarPacienteAsync(Paciente paciente)
        {
            OperationResult result = new OperationResult();
            {
                try
                {
                    using (var conn = new SqlConnection(_conexionBD))
                    {
                        await conn.OpenAsync();
                        using (var cmd = new SqlCommand("sp_ActualizarPaciente", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Cedula", paciente.Cedula);

                            //Actualizar paciente
                            cmd.Parameters.AddWithValue("@Direccion", paciente.Direccion);
                            cmd.Parameters.AddWithValue("@Ocupacion", paciente.Ocupacion);
                            cmd.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                            cmd.Parameters.AddWithValue("@OperacionesPrevias", paciente.OperacionesPrevias);
                            cmd.Parameters.AddWithValue("@AntecedentesFamiliares", paciente.AntecedentesFamiliares);

                            //hacer que no se envien en string mas que el campo que quiero actualizar
                            string jsonAntecedentes = "[]";
                            if (paciente.AntecedentesPatologicos != null && paciente.AntecedentesPatologicos.Any())
                            {
                                jsonAntecedentes = JsonSerializer.Serialize(paciente.AntecedentesPatologicos);
                            }
                            cmd.Parameters.AddWithValue("@AntecedentesPatologicosJson", jsonAntecedentes);

                            object? rowsAffected = await cmd.ExecuteScalarAsync();
                            int newNum = Convert.ToInt32(rowsAffected);

                            if (newNum > 0)
                            {
                                _logger.LogInformation("Paciente actualizado exitosamente.");
                                result = OperationResult.Success("Paciente actualizado exitosamente.");
                            }
                            else
                            {
                                _logger.LogWarning("No se pudo actualizar el paciente.");
                                result = OperationResult.Failure("No se pudo actualizar el paciente.");
                            }
                        }
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = $"Ocurrio un error al actualizar el paciente. {ex.Message}";
                    _logger.LogError(ex, "Ocurrio un error al actualizar el paciente.");
                }
                return result;

            }
        }

        public async Task<OperationResult> CrearPacienteAsync(Paciente paciente)
        {
            OperationResult result = new OperationResult();
            {
                try
                {
                     string jsonAntecedentes = "[]";
                    if (paciente.AntecedentesPatologicos != null && paciente.AntecedentesPatologicos.Any())
                    {
                        jsonAntecedentes = JsonSerializer.Serialize(paciente.AntecedentesPatologicos);
                    }

                    //Insertar paciente
                    var presult = await ExecuteStoredProcedureAsync("sp_CrearPacientes", new SqlParameter("@Cedula", paciente.Cedula), new SqlParameter("@Nombre", paciente.Nombre), new SqlParameter("@Apellido", paciente.Apellido), new SqlParameter("@FechaNacimiento", paciente.FechaNacimiento), new SqlParameter("@Genero", paciente.Genero), new SqlParameter("@Nacionalidad", paciente.Nacionalidad), new SqlParameter("@Direccion", paciente.Direccion), new SqlParameter("@Ocupacion", paciente.Ocupacion), new SqlParameter("@Telefono", paciente.Telefono), new SqlParameter("@OperacionesPrevias", paciente.OperacionesPrevias), new SqlParameter("@AntecedentesFamiliares", paciente.AntecedentesFamiliares), new SqlParameter("@UsuarioRegistro", paciente.UsuarioRegistro), new SqlParameter("@AntecedentesPatologicosJson", jsonAntecedentes));  

                    if (presult > 0)
                    {
                        _logger.LogInformation("Paciente creado exitosamente.");
                        result = OperationResult.Success("Paciente creado exitosamente.");
                    }
                    else
                    {
                        _logger.LogWarning("No se pudo crear el paciente.");
                        result = OperationResult.Failure("No se pudo crear el paciente.");
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = $"Ocurrio un error al crear el paciente. {ex.Message}";
                    _logger.LogError(ex, "Ocurrio un error al crear el paciente.");
                }
                return result;
            }
        }

        public async Task<bool> ExistePorCedulaAsync(string cedula)
        {
            OperationResult result = new OperationResult();
            {

                _logger.LogInformation("Validando existencia del paciente con cédula: {Cedula}", cedula);
                using (var conn = new SqlConnection(_conexionBD))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand("sp_ValidarPaciente", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Cedula", cedula);
                        var exists = await cmd.ExecuteScalarAsync();
                        return exists != null;
                    }
                }

            }
        }

        public async Task<OperationResult> ObtenerPorCedulaAsync(string cedula)
        {
            OperationResult result = new OperationResult();
            {
                try
                {
                    _logger.LogInformation("Obteniendo paciente por cédula: {Cedula}", cedula);
                    using (var conn = new SqlConnection(_conexionBD))
                    {
                        await conn.OpenAsync();
                        using (var cmd = new SqlCommand("sp_ObtenerPacientePorCedula", conn)) 
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Cedula", cedula);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                Paciente? paciente = null;
                                if (await reader.ReadAsync())
                                {
                                    paciente = new Paciente
                                    {
                                        Cedula = reader.GetString(reader.GetOrdinal("Cedula")),
                                        Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                        Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                                        FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
                                        Edad = (DateTime.Now.Year - reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")).Year).ToString(),
                                        Genero = reader.IsDBNull(reader.GetOrdinal("Genero")) ? "" : reader.GetString(6),
                                        Nacionalidad = reader.GetString(reader.GetOrdinal("Nacionalidad")),
                                        Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                        Ocupacion = reader.GetString(reader.GetOrdinal("Ocupacion")),
                                        Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                        OperacionesPrevias = reader.GetString(reader.GetOrdinal("OperacionesPrevias")),
                                        AntecedentesFamiliares = reader.GetString(reader.GetOrdinal("AntecedentesFamiliares")),
                                        FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                                        UsuarioRegistro = reader.GetInt32(reader.GetOrdinal("UsuarioRegistro")),
                                        Activo = !reader.IsDBNull(reader.GetOrdinal("Activo")) && reader.GetBoolean(reader.GetOrdinal("Activo")),
                                        AntecedentesPatologicos = new List<string>()
                                    };

                                }

                                if (paciente == null)
                                {
                                    _logger.LogWarning("Paciente no encontrado con cédula: {Cedula}", cedula);
                                    return OperationResult.Failure("Paciente no encontrado.");
                                }
                                else
                                {
                                    if (await reader.NextResultAsync())
                                    {
                                        while (await reader.ReadAsync())
                                        {
                                            string antecedente = reader.GetString(reader.GetOrdinal("Antecedente"));
                                            paciente.AntecedentesPatologicos.Add(reader.GetString(reader.GetOrdinal("Antecedente")));
                                        }
                                    }
                                }

                                _logger.LogInformation("Paciente encontrado: {Paciente}", paciente.Cedula);
                                return OperationResult.Success("Paciente encontrado.", paciente);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = $"Ocurrio un error al obtener el paciente. {ex.Message}";
                    _logger.LogError(ex, "Ocurrio un error al obtener el paciente por cédula: {Cedula}", cedula);
                    return OperationResult.Failure(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<Paciente>> ObtenerPorNombreAsync(string nombre)
        {
            _ = new OperationResult();
            OperationResult result;
            List<Paciente> pacientes = new List<Paciente>();

            try
            {
                _logger.LogInformation("Obteniendo pacientes por nombre: {Nombre}", nombre);
                using(var conn = new SqlConnection(_conexionBD))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand("sp_ObtenerPacientesPorNombre", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Paciente paciente = new Paciente
                                {
                                    Cedula = reader.GetString(reader.GetOrdinal("Cedula")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                                    FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
                                    Edad = (DateTime.Now.Year - reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")).Year).ToString(),
                                    Genero = reader.IsDBNull(reader.GetOrdinal("Genero")) ? "" : reader.GetString(6),
                                    Nacionalidad = reader.GetString(reader.GetOrdinal("Nacionalidad")),
                                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    Ocupacion = reader.GetString(reader.GetOrdinal("Ocupacion")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    OperacionesPrevias = reader.GetString(reader.GetOrdinal("OperacionesPrevias")),
                                    AntecedentesFamiliares = reader.GetString(reader.GetOrdinal("AntecedentesFamiliares")),
                                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                                    UsuarioRegistro = reader.GetInt32(reader.GetOrdinal("UsuarioRegistro")),
                                    Activo = !reader.IsDBNull(reader.GetOrdinal("Activo")) && reader.GetBoolean(reader.GetOrdinal("Activo")),
                                    AntecedentesPatologicos = new List<string>()
                                };
                                pacientes.Add(paciente);
                            }
                            if (pacientes.Count == 0)
                            {
                                _logger.LogWarning("No se encontraron pacientes con el nombre: {Nombre}", nombre);
                                result = OperationResult.Failure("No se encontraron pacientes con ese nombre.");
                            }
                            else
                            {
                                _logger.LogInformation("{Count} pacientes encontrados con el nombre: {Nombre}", pacientes.Count, nombre);
                                result = OperationResult.Success($"{pacientes.Count} pacientes encontrados.", pacientes);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Ocurrio un error al obtener los pacientes. {ex.Message}");
                _logger.LogError(ex, "Ocurrio un error al obtener los pacientes por nombre: {Nombre}", nombre);
            }
            return pacientes;
        }

        private async Task<int> ExecuteStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters)
        {
            object? resultadoId;
            int nuevoId;
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
                    resultadoId = await cmd.ExecuteScalarAsync();
                    nuevoId = Convert.ToInt32(resultadoId);
                    return nuevoId;
                }
            }
        }
    }
}
