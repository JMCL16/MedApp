using MedApp.Application.DTOs.Login;
using MedApp.Domain.Base;

namespace MedApp.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<OperationResult> LoginAsync(LoginDTO loginDTO);
        Task<OperationResult> RegisterAsync(LoginDTO registerDTO);
        Task<OperationResult> UpdateRolAsync(ActualizarRolDTO actualizarRolDTO);
    }
}
