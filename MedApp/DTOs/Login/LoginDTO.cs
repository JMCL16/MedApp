using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.DTOs.Login
{
    public class LoginDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}
