using MedApp.Application.DTOs.Consultas;
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
                    CedulaPaciente = consultaDTO.CedulaPaciente,
                    FechaConsulta = consultaDTO.FechaConsulta,
                    Diagnostico = consultaDTO.Diagnostico,
                    Tratamiento = consultaDTO.Tratamiento
                };
            }
        }

        public static Consulta MapToUpdateEntity(ConsultaUpdateDTO consultaUpdateDTO)
        {
            {
                return new Consulta
                {
                    IdConsulta = consultaUpdateDTO.Id,
                    CedulaPaciente = consultaUpdateDTO.CedulaPaciente,
                    FechaConsulta = consultaUpdateDTO.FechaConsulta,
                    Diagnostico = consultaUpdateDTO.Diagnostico,
                    Tratamiento = consultaUpdateDTO.Tratamiento
                };
            }
        }
    }
}
