using MedApp.Services;
using System;
using System.Windows.Forms;

namespace MedApp
{
    public partial class Login : Form
    {
        public readonly AuthService _authService;

        public Login()
        {
            InitializeComponent();
            _authService = new AuthService();

        }


        private async void SignBtn_Click(object sender, EventArgs e)
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Ingrese el nombre de usuario", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            // Mostrar indicador de carga
            SignBtn.Enabled = false;
            SignBtn.Text = "Iniciando sesión...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var usuario = await _authService.LoginAsync(txtUsuario.Text.Trim(), txtContrasena.Text);
                if (usuario != null)
                {
                    // Guardar sesión
                    Sesion.IniciarSesion(usuario.Id, usuario.UserName, usuario.Rol);
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
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar iniciar sesión: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SignBtn.Enabled = true;
                SignBtn.Text = "Iniciar Sesión";
                this.Cursor = Cursors.Default;
            }
        }
    }
}
