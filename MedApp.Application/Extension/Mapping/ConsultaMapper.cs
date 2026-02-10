using MedApp.Application.DTOs;
using MedApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.Extension.Mapping
{
    public static class ConsultaMapper
    {
        public static Consulta MapToCreateEntity(ConsultaDTO consultaDTO)
        {
            {
                return new Consulta
                {
                    PacienteId = consultaDTO.PacienteId,
                    CedulaPaciente = consultaDTO.CedulaPaciente,
                    FechaConsulta = consultaDTO.FechaConsulta,
                    Diagnostico = consultaDTO.Diagnostico,
                    Tratamiento = consultaDTO.Tratamiento
                };
            }
        }
    }
}
