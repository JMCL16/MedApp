using MedApp.Application.DTOs.Consultas;
using MedApp.Domain.Base;
using MedApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.Interfaces.Repositories
{
    public interface IConsultaRepository
    {
        Task<OperationResult> CrearConsultaAsync(Consulta consulta);
        Task<OperationResult> ActualizarConsultaAsync(Consulta consulta);
        Task<IEnumerable<ConsultaDTO>> ObtenerPorCedulaAsync(string cedula);
        Task<IEnumerable<Consulta>> ObtenerPorPacienteIdAsync(int pacienteId);
    }
}
