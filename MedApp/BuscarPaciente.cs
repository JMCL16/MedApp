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
        private PacienteService pacienteService;
        private List<Paciente> pacientesEncontrados;
        public Paciente  PacienteSeleccionado { get; private set; }
        public BuscarPaciente()
        {
            InitializeComponent();
            ConexionBD conexionBD = new ConexionBD();
            pacienteService = new PacienteService(conexionBD);
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txtBusqueda.Text.Trim();

            if (string.IsNullOrWhiteSpace(criterioBusqueda))
            {
                MessageBox.Show("Ingrese un criterio de búsqueda", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar por cédula o nombre
            if (rbCedula.Checked)
            {
                Paciente paciente = pacienteService.ObtenerPacientePorCedula(criterioBusqueda);
                pacientesEncontrados = paciente != null ? new List<Paciente> { paciente } : new List<Paciente>();
                txtBusqueda.Focus();
            }
            else
            {
                pacientesEncontrados = pacienteService.BuscarPacientesPorNombre(criterioBusqueda);
                
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

            PacienteSeleccionado = (Paciente)dgvPacientes.SelectedRows[0].DataBoundItem;
            this.DialogResult = DialogResult.OK;
            this.Close();
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
