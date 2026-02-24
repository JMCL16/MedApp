using MedApp.Application.Interfaces.IRepositories;
using MedApp.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using MedApp.Domain.Base;
using System.Runtime.InteropServices;

namespace MedApp.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _conexionBD;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthRepository(IConfiguration configuration, ILogger<User> logger)
        {
            _configuration = configuration;
            _conexionBD = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("connection string not found");
            _logger = logger;
        }

        public async Task<OperationResult> actualizarRolUsuarioAsync(int usuarioId, string nuevoRol)
        {
            _ = new OperationResult();
            OperationResult result;

            try
            {
                using (var conn = new SqlConnection(_conexionBD))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarRolUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", usuarioId);
                        cmd.Parameters.AddWithValue("@NuevoRol", nuevoRol);
                        int rowsAffected = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        if (rowsAffected > 0)
                        {
                            _logger.LogInformation("Rol del usuario con ID {UserId} actualizado a {NewRole}", usuarioId, nuevoRol);
                            result = OperationResult.Success("Rol actualizado exitosamente");
                            return result;
                        }
                        else
                        {
                            _logger.LogWarning("No se encontró el usuario con ID {UserId} para actualizar el rol", usuarioId);
                            result = OperationResult.Failure("Usuario no encontrado para actualizar el rol");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al actualizar el rol del usuario con ID {UserId}", usuarioId);
                result = OperationResult.Failure("Excepción al actualizar el rol del usuario: " + ex.Message);
            }
            return result;
        }

        public async Task<OperationResult> crearUsuarioAsync(User user)
        {
            _ = new OperationResult();
            OperationResult result;
            try
            {
                using (var conn = new SqlConnection(_conexionBD))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_CrearUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", user.UserName);
                        cmd.Parameters.AddWithValue("@PasswordKey", user.PasswordKey);
                        cmd.Parameters.AddWithValue("@Roles", user.Roles);
                        cmd.Parameters.AddWithValue("@Activo", user.Activo);
                        var rowsAffected = await cmd.ExecuteScalarAsync();
                        if (rowsAffected != null && int.TryParse(rowsAffected.ToString(), out int newUserId))
                        {
                            user.Id = newUserId;
                            _logger.LogInformation("Usuario creado con ID: {UserId}", newUserId);
                            result = OperationResult.Success("Usuario creado exitosamente", user.UserName);
                            return result;
                        }
                        else
                        {
                            _logger.LogError("Error al crear el usuario");
                            result = OperationResult.Failure("Error al crear el usuario");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al crear el usuario");
                result = OperationResult.Failure("Excepción al crear el usuario: " + ex.Message);
            }
            return result;
        }

        public async Task<User?> validateUserAsync(string user)
        {
            try
            {
                using (var conn = new SqlConnection(_conexionBD))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarioPorNombre", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", user);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                User usuario = new User
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                    PasswordKey = reader.GetString(reader.GetOrdinal("PasswordKey")),
                                    Roles = reader.GetString(reader.GetOrdinal("Roles")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };
                                _logger.LogInformation("Usuario encontrado: {UserName}", usuario.UserName);
                               return usuario;
                            }
                            else
                            {
                                _logger.LogWarning("Usuario no encontrado: {UserName}", user);
                                return null;
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar el usuario");
                throw;
            }
        }
    }
}
