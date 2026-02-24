using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.DTOs.Login
{
    public class LoginDTO
    {
        public string UserName { get; set; } = string.Empty;
        public required string Password { get; set; }
    }
}
