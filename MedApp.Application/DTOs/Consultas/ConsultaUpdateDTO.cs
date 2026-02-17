using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.DTOs.Consultas
{
    public class ConsultaUpdateDTO
    {
        [DefaultValue("")] public int Id { get; set; }
        [DefaultValue("")] public string? CedulaPaciente { get; set; }
        public DateTime FechaConsulta { get; set; }
        [DefaultValue("")] public string? Diagnostico { get; set; }
        [DefaultValue("")] public string? Tratamiento { get; set; }
    }
}
