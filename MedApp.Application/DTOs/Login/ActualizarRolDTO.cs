using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.DTOs.Login
{
    public class ActualizarRolDTO
    {
        public int IdUsuario { get; set; }
        public string NuevoRol { get; set; } = string.Empty;
    }
}
