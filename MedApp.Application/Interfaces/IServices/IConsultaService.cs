using MedApp.Application.DTOs.Consultas;
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
        Task<OperationResult> ActualizarConsultaAsync(ConsultaUpdateDTO consultaDto);
        Task<OperationResult> ObtenerPorCedulaAsync(string cedula);
        Task<OperationResult> ObtenerPorPacienteIdAsync(int pacienteId);

    }
}
