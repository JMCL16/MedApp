using System;
using System.Windows.Forms;

namespace MedApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            ConfigurarPermisos();
            //MostrarInformacionUsuario();
        }

        private void ConfigurarPermisos()
        {
            if (!Sesion.EstaLogueado)
            {
                MessageBox.Show("Debe iniciar sesion", "Acceso denegado");
                this.Close();
                return;
            }

            if (string.IsNullOrEmpty(Sesion.Rol))
            {
                MessageBox.Show("No se ha asignado un rol al usuario. Acceso limitado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            gestionarUserBtn.Visible = false;


            switch (Sesion.Rol)
            {
                case "admin":
                    gestionarUserBtn.Visible = true;
                    break;
                case "Medico":
                    gestionarUserBtn.Visible = false;
                    break;
                case "Secretario":
                    gestionarUserBtn.Visible = false;
                    newConsultaBtn.Visible = false;
                    break;
                default:
                    MessageBox.Show($"Rol desconocido. ('{Sesion.Rol}'). Acceso limitado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

        }
        

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cerrar sesión al cerrar el formulario
            Sesion.CerrarSesion();
        }

        private void CloseSessionBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Sesion.CerrarSesion();
                this.Close();
            }
        }
     }
}
