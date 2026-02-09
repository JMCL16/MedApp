using MedApp.Domain.Base;
using MedApp.Domain.Models;

namespace MedApp.Application.Interfaces.Repositories
{
    public interface IPacienteRepository
    {
        Task<OperationResult> CrearPacienteAsync(Paciente paciente);
        Task<OperationResult> ActualizarPacienteAsync(Paciente paciente);
        Task<OperationResult> ObtenerPorCedulaAsync(string cedula);
        Task<IEnumerable<Paciente>> ObtenerPorNombreAsync(string nombre);
        Task<bool> ExistePorCedulaAsync(string cedula);
    }
}
