using MedApp.Presentation.DTOs.Consultas;
using MedApp.Presentation.DTOs.Paciente;
using MedApp.Services;
using System;
using System.Windows.Forms;

namespace MedApp
{
    public partial class NewConsult : Form
    {
        private readonly ConsultaDTO consultaDTO;
        private readonly ConsultaService _consultaService;
        public NewConsult(PacienteDTO paciente)
        {
            InitializeComponent();
            _consultaService = new ConsultaService();
        }
        
        public NewConsult()
        {
            InitializeComponent();
             _consultaService = new ConsultaService();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool consulta = await _consultaService.CrearConsulta(consultaDTO);
                if (consulta)
                {
                    MessageBox.Show("Consulta creada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    new Main().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al crear la consulta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
               MessageBox.Show($"Error al crear la consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
