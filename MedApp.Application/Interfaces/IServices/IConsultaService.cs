using MedApp.Application.DTOs;
using MedApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.Interfaces.IServices
{
    public interface IConsultaService
    {
        Task<OperationResult> CrearConsultaAsync(ConsultaDTO consultaDto);
        Task<OperationResult> ActualizarConsultaAsync(ConsultaDTO consultaDto);
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerPorPacienteIdAsync(int pacienteId);

    }
}
