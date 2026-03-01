using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.DTOs.User
{
    public class CrearUsuarioDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }
        public string Roles { get; set; } = string.Empty;
    }
}
