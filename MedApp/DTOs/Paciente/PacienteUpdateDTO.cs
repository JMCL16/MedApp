
using System.Collections.Generic;
using System.ComponentModel;

namespace MedApp.Presentation.DTOs.Paciente
{
    public class PacienteUpdateDTO
    {
        [DefaultValue("")] public string Cedula { get; set; } = string.Empty;
        [DefaultValue("")] public string Direccion { get; set; } = string.Empty;
        [DefaultValue("")] public string Ocupacion { get; set; } = string.Empty;
        [DefaultValue("")] public string Telefono { get; set; } = string.Empty;
        // Antecedentes medicos 
        [DefaultValue("")] public string OperacionesPrevias { get; set; } = string.Empty;
        [DefaultValue("")] public string AntecedentesFamiliares { get; set; } = string.Empty;
        public List<string> AntecedentesPatologicos { get; set; } = new List<string>();
    }
}
