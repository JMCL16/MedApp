using System;
using System.Windows.Forms;

namespace MedApp
{
    public partial class Login : Form
    {
        private ConexionBD conexionBD;
        public Login()
        {
            InitializeComponent();
            conexionBD = new ConexionBD();

        }


        private void SignBtn_Click(object sender, EventArgs e)
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Ingrese el nombre de usuario", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Ingrese la contraseña", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Focus();
                return;
            }

            // Mostrar indicador de carga
            SignBtn.Enabled = false;
            SignBtn.Text = "Iniciando sesión...";
            this.Cursor = Cursors.WaitCursor;

            // Autenticar
            User usuario = conexionBD.Autenticar(
                txtUsuario.Text.Trim(),
                txtContrasena.Text
            );

            // Restaurar UI
            SignBtn.Enabled = true;
            SignBtn.Text = "Iniciar Sesión";
            this.Cursor = Cursors.Default;

            if (usuario != null)
            {
                // Guardar sesión
                SesionActual.usuarioActual = usuario;

                // Cerrar login y abrir formulario principal
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error de autenticación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContrasena.Clear();
                txtContrasena.Focus();
            }
        }
    }
}
