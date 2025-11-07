using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp
{
    public enum RolUsuario
    {
        Admin = 1,
        Medico = 2,
        Secretario = 3
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordKey { get; set; }
        public string Roles {  get; set; }
        public bool Activo { get; set; }

        public RolUsuario RolEnum
        {
            get
            {
                if (string.IsNullOrEmpty(Roles))
                    return RolUsuario.Secretario;

                switch (Roles.ToLower().Trim()) // Agregar Trim() por si hay espacios
                {
                    case "admin":
                        return RolUsuario.Admin;
                    case "medico":
                        return RolUsuario.Medico;
                    case "secretario":
                        return RolUsuario.Secretario;
                    default:
                        return RolUsuario.Secretario;
                }
            }
        }

    }

    public static class SesionActual
    {
        public static User usuarioActual { get; set; }
        public static bool EstaAutenticado()
        {
            return usuarioActual != null;
        }
        public static bool isAdmin()
        { return usuarioActual != null && usuarioActual.RolEnum == RolUsuario.Admin; }

        public static bool isMedico() {
            return usuarioActual != null && usuarioActual.RolEnum == RolUsuario.Medico;
        }

        public static bool isSecretario()
        {
            return usuarioActual != null && usuarioActual.RolEnum == RolUsuario.Secretario;
        }

        public static bool TienePermiso(params RolUsuario[] rolesPermitidos)
        {
            if(usuarioActual == null) return false;
            return rolesPermitidos.Contains(usuarioActual.RolEnum);
        }

        public static void CerrarSesion()
        {
            usuarioActual = null;
        }
     }
}
