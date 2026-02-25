using MedApp.Presentation.DTOs.Paciente;
using System;
using System.Windows.Forms;

namespace MedApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
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
                    newConsultaBtn.Enabled = false;
                    break;
                default:
                    MessageBox.Show($"Rol desconocido. ('{Sesion.Rol}'). Acceso limitado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

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

        private void newPacientBtn_Click(object sender, EventArgs e)
        {
            NewPacient newPacient = new NewPacient();
            DialogResult result = newPacient.ShowDialog();
            if (result == DialogResult.OK)
            {
                MessageBox.Show("Paciente creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void searchPacientBtn_Click(object sender, EventArgs e)
        {
            BuscarPaciente buscarPaciente = new BuscarPaciente();
            DialogResult result = buscarPaciente.ShowDialog();
        }

        private void newConsultaBtn_Click(object sender, EventArgs e)
        {
            NewConsult newConsult = new NewConsult();
            DialogResult result = newConsult.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = $"Bienvenido/a, {Sesion.UserName}";
            ConfigurarPermisos();
        }

        private void gestionarUserBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
