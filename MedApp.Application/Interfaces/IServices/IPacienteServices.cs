using MedApp.Application.DTOs.Paciente;
using MedApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.Interfaces.IServices
{
    public interface IPacienteServices
    {
        Task<OperationResult> CrearPacienteAsync(PacienteDTO pacienteDto);
        Task<OperationResult> ActualizarPacienteAsync(PacienteUpdateDTO pacienteUpdateDto);
        Task<OperationResult> ObtenerPorCedulaAsync(string cedula);
        Task<OperationResult> ObtenerPorNombreAsync (string nombre);
    }
}
