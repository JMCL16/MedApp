using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Presentation.DTOs.Paciente
{
    public class PacienteDTO
    {
        public string Cedula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Edad { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Ocupacion { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        // Antecedentes medicos 
        public string OperacionesPrevias { get; set; } = string.Empty;
        public string AntecedentesFamiliares { get; set; } = string.Empty;
        public List<string> AntecedentesPatologicos { get; set; } = new List<string>();

        public DateTime FechaRegistro { get; set; }
        public int UsuarioRegistro { get; set; }
        public bool Activo { get; set; } = true;

        public string NombreCompleto
        {
            get { return string.Format("{0} {1}", Nombre, Apellido); }
        }

    }
}
