using MedApp.Presentation.DTOs.Paciente;
using MedApp.Presentation.Services;
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
    public partial class BuscarPaciente : Form
    {
        private PacienteService _service;
        private List<PacienteDTO> pacientesEncontrados;
        public PacienteDTO  PacienteSeleccionado { get; private set; }
        public BuscarPaciente()
        {
            InitializeComponent();
            _service = new PacienteService();
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvPacientes.AutoGenerateColumns = false;
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPacientes.MultiSelect = false;

            // Configurar columnas
            dgvPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Cedula",
                HeaderText = "Cédula",
                Width = 120
            });

            dgvPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreCompleto",
                HeaderText = "Nombre Completo",
                Width = 200
            });

            dgvPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaNacimiento",
                HeaderText = "Fecha Nacimiento",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Edad",
                HeaderText = "Edad",
                Width = 60
            });

            dgvPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono",
                Width = 120
            });
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txtBusqueda.Text.Trim();

            if (string.IsNullOrWhiteSpace(criterioBusqueda))
            {
                MessageBox.Show("Ingrese los datos para el criterio de búsqueda", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar por cédula o nombre
            if (rbCedula.Checked)
            {
                PacienteDTO paciente = await _service.ObtenerPacientePorCedula(criterioBusqueda);
                pacientesEncontrados = paciente != null ? new List<PacienteDTO> { paciente } : new List<PacienteDTO>();
                txtBusqueda.Focus();
            }
            else
            {
                pacientesEncontrados = await _service.BuscarPorNombre(criterioBusqueda);
            }

            dgvPacientes.DataSource = null;
            dgvPacientes.DataSource = pacientesEncontrados;

            if (pacientesEncontrados.Count == 0)
            {
                MessageBox.Show("No se encontraron pacientes", "Búsqueda",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un paciente", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmar = MessageBox.Show("Desea crear una consulta con el paciente seleccionado", Text, MessageBoxButtons.YesNo);
            if (confirmar == DialogResult.Yes)
            {
                PacienteSeleccionado = (PacienteDTO)dgvPacientes.CurrentRow.DataBoundItem;
                NewConsult newConsult = new NewConsult(PacienteSeleccionado);
                if (newConsult.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                var verConsulta = MessageBox.Show("Desea ver las consultas del paciente seleccionado", Text, MessageBoxButtons.YesNo);
                if (verConsulta == DialogResult.Yes)
                {
                    PacienteSeleccionado = (PacienteDTO)dgvPacientes.CurrentRow.DataBoundItem;
                    ViewConsultas viewConsultas = new ViewConsultas(PacienteSeleccionado);
                    if (viewConsultas.ShowDialog() == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }                
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvPacientes_DoubleClick(object sender, EventArgs e)
        {
            btnSeleccionar_Click(sender, e);
        }

        private void rbNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNombre.Checked) {
                txtBusqueda.Focus();
            }
        }

        private void rbCedula_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCedula.Checked) { txtBusqueda.Focus(); }
        }
    }
}
