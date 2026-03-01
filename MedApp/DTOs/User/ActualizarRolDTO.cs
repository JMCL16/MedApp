using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.DTOs.User
{
    public class ActualizarRolDTO
    {
        public int IdUsuario { get; set; }
        public string NuevoRol { get; set; } = string.Empty;
    }
}
