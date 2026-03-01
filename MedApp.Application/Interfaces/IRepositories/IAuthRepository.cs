using MedApp.Domain.Base;
using MedApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<User?> validateUserAsync (string user);
        Task<OperationResult> crearUsuarioAsync (User user);
        Task<OperationResult> actualizarRolUsuarioAsync (int usuarioId, string nuevoRol);
        Task<List<User>> ObtenerTodosUsuariosAsync();
    }
}
