using MedApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedApp.Domain.Models;

namespace MedApp.Application.Extension.Mapping
{
    public static class PacienteMapper
    {
        public static Paciente MapToEntityCreate(PacienteDTO paciente)
        {
            {
                return new Paciente
                {
                    Cedula = paciente.Cedula,
                    Nombre = paciente.Nombre,
                    Apellido = paciente.Apellido,
                    FechaNacimiento = paciente.FechaNacimiento,
                    Edad = paciente.Edad,
                    Genero = paciente.Genero,
                    Nacionalidad = paciente.Nacionalidad,
                    Direccion = paciente.Direccion,
                    Ocupacion = paciente.Ocupacion,
                    Telefono = paciente.Telefono,
                    OperacionesPrevias = paciente.OperacionesPrevias,
                    AntecedentesFamiliares = paciente.AntecedentesFamiliares,
                    AntecedentesPatologicos = paciente.AntecedentesPatologicos,
                    FechaRegistro = DateTime.Now,
                    UsuarioRegistro = paciente.UsuarioRegistro,
                    Activo = true
                };
            }
        }
    }
}
