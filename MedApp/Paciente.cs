using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Edad { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
        public string Direccion { get; set; }
        public string Ocupacion {  get; set; }

        public string Telefono { get; set; }

        // Antecedentes medicos 
        public string OperacionesPrevias { get; set; }
        public string AntecedentesFamiliares { get; set; } 
        public List<string> AntecedentesPatologicos { get; set; } = new List<string>();

        public DateTime FechaRegistro { get; set; }
        public int UsuarioRegistro {  get; set; }
        public bool Activo { get; set; } = true;

        public string NombreCompleto
        {
            get { return string.Format("{0} {1}", Nombre, Apellido); }
        }
    }
}
