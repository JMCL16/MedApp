using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (!SesionActual.EstaAutenticado())
            {
                MessageBox.Show("Debe iniciar sesion", "Acceso denegado");
                this.Close();
                return;
            }

            gestionarUserBtn.Visible = SesionActual.isAdmin();

            newPacientBtn.Enabled = SesionActual.TienePermiso(RolUsuario.Admin, RolUsuario.Medico);
            newConsulta.Enabled = SesionActual.TienePermiso(RolUsuario.Admin, RolUsuario.Medico);
            searchPacientBtn.Enabled = true;

        }
        private void newPacientBtn_Click(object sender, EventArgs e)
        {
            if (!SesionActual.TienePermiso(RolUsuario.Admin, RolUsuario.Medico))
            {
                MessageBox.Show("No tiene permisos para registrar pacientes",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var formRegistro = new NewPacient();
            formRegistro.ShowDialog();
            this.Hide();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cerrar sesión al cerrar el formulario
            SesionActual.CerrarSesion();
        }

        private void CloseSessionBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SesionActual.CerrarSesion();
                this.Close();
            }
        }

        private void newConsulta_Click(object sender, EventArgs e)
        {
            if (!SesionActual.TienePermiso(RolUsuario.Admin, RolUsuario.Medico))
            {
                MessageBox.Show("No tiene permisos para registrar consultas",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Debe buscar un paciente registrado", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void searchPacientBtn_Click(object sender, EventArgs e)
        {
            using (BuscarPaciente formBuscar = new BuscarPaciente()) { 
                if (formBuscar.ShowDialog() == DialogResult.OK)
                {
                    Paciente pacienteSeleccionado = formBuscar.PacienteSeleccionado;

                    if (pacienteSeleccionado != null)
                    {

                        NewConsult consult = new NewConsult(pacienteSeleccionado);
                        consult.ShowDialog();
                    }
                    else {
                        MessageBox.Show("No se seleccionó ningún paciente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            
            } 


        }
    }
}
