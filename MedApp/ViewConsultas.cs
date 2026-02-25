using MedApp.Presentation.DTOs.Paciente;
using MedApp.Services;
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
    public partial class ViewConsultas : Form
    {
        private readonly PacienteDTO _pacienteActual;
        private readonly ConsultaService _consultaService;
        public ViewConsultas(PacienteDTO pacienteSeleccionado)
        {
            InitializeComponent();
            _consultaService = new ConsultaService();
            _pacienteActual = pacienteSeleccionado;
        }

        private async void ViewConsultas_Load(object sender, EventArgs e)
        {
            var listaConsultas = await _consultaService.ObtenerConsultaPorCedula(_pacienteActual.Cedula);
            dgvConsultas.DataSource = null;
            dgvConsultas.DataSource = listaConsultas;
            if (listaConsultas == null || listaConsultas.Count == 0)
            {
                MessageBox.Show("Este paciente aun no tiene consultas registradas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
