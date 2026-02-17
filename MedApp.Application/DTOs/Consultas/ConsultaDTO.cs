using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.DTOs.Consultas
{
    public class ConsultaDTO
    {
        public int Id { get; set; }
        public string? CedulaPaciente { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string? Diagnostico { get; set; }
        public string? Tratamiento { get; set; }
    }
}
