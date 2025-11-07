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
    public partial class NewConsult : Form
    {
        private Paciente _paciente;
        private ConsultaService consultaService;
        public NewConsult(Paciente paciente)
        {
            InitializeComponent();
            _paciente = paciente;
            ConexionBD conexionBD = new ConexionBD();
            consultaService = new ConsultaService(conexionBD);

            txtNombreCompleto.Text = _paciente.NombreCompleto;
            txtCedula.Text = _paciente.Cedula;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Consulta consulta = new Consulta()
            {
                PacienteId = _paciente.Id,
                FechaConsulta = DateTime.Now,
                Diagnostico = rtbDiagnostico.Text,
                Tratamiento = rtbTratamiento.Text,
            };

            try
            {
                consultaService.GuardarConsulta(consulta);
                MessageBox.Show("Consulta guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la consulta: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar la consulta?",
            "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                new Main().Show();
                this.Close();
            }
        }
    }
}
