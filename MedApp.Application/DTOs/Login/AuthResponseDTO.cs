using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.DTOs.Login
{
    public class AuthResponseDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? Rol { get; set; } = string.Empty;
    }
}
