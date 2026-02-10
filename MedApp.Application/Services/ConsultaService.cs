

using MedApp.Application.DTOs;
using MedApp.Application.Interfaces.IServices;
using MedApp.Domain.Base;

namespace MedApp.Application.Services
{
    public class ConsultaService : IConsultaService
    {
        
        public ConsultaService() { }
        public Task<OperationResult> ActualizarConsultaAsync(ConsultaDTO consultaDto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> CrearConsultaAsync(ConsultaDTO consultaDto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorPacienteIdAsync(int pacienteId)
        {
            throw new NotImplementedException();
        }
    }
}
