using MedApp.Application.DTOs.Login;
using MedApp.Application.Interfaces.IRepositories;
using MedApp.Application.Interfaces.IServices;
using MedApp.Domain.Base;
using MedApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<OperationResult> LoginAsync(LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.UserName) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return OperationResult.Failure("El usuario y contraseña son requeridos");
            }

            var user = await _authRepository.validateUserAsync(loginDTO.UserName);
            if (user == null)
            {
                return OperationResult.Failure("Las credenciales son incorrectas.");
            }
            var hashPassword = Encriptar(loginDTO.Password);

            if (user.PasswordKey != hashPassword)
            {
                return OperationResult.Failure("Las credenciales son incorrectas.");
            }

            var response = new AuthResponseDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Rol = user.Roles
            };

            return OperationResult.Success("Login exitoso.", response);
        }

        public async Task<OperationResult> RegisterAsync(LoginDTO registerDTO)
        {
            if (string.IsNullOrEmpty(registerDTO.UserName) || string.IsNullOrEmpty(registerDTO.Password))
            {
                return OperationResult.Failure("El usuario y contraseña son requeridos");
            }
            var userExistente = await _authRepository.validateUserAsync(registerDTO.UserName);
            if (userExistente != null)
            {
                return OperationResult.Failure("El nombre de usuario ya existe.");
            }

            var nuevoUsuario = new User
            {
                UserName = registerDTO.UserName,
                PasswordKey = Encriptar(registerDTO.Password),
                Roles = "Medico",
                Activo = true
            };

            var usuarioCreado = await _authRepository.crearUsuarioAsync(nuevoUsuario);
            return OperationResult.Success("Usuario registrado exitosamente.", usuarioCreado);
        }

        public async Task<OperationResult> UpdateRolAsync(ActualizarRolDTO actualizarRolDTO)
        {
            var rolesPermitidos = new List<string> { "Admin", "Medico", "Secretario" };
            if (!rolesPermitidos.Contains(actualizarRolDTO.NuevoRol))
            {
                return OperationResult.Failure("El rol proporcionado no es válido.");
            }
            var exito = await _authRepository.actualizarRolUsuarioAsync(actualizarRolDTO.IdUsuario, actualizarRolDTO.NuevoRol);
            if (exito == null)
            {
                return OperationResult.Failure("No se pudo actualizar el rol del usuario.");
            }
            return OperationResult.Success("Rol del usuario actualizado exitosamente.", exito);
        }

        private string Encriptar(string texto)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
