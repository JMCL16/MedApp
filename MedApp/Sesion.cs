using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp
{
    public static class Sesion
    {
        public static int IdUsuario { get; private set; }
        public static string UserName { get; private set; }
        public static string Rol { get; private set; }

        public static void IniciarSesion(int idUsuario, string userName, string rol)
        {
            IdUsuario = idUsuario;
            UserName = userName;
            Rol = rol;
        }

        public static void CerrarSesion()
        {
            IdUsuario = 0;
            UserName = string.Empty;
            Rol = string.Empty;
        }

        public static bool EstaLogueado => IdUsuario > 0;
    }
}
