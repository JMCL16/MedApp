using MedApp.Presentation.DTOs.Paciente;
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
    public partial class DatosPersonales : UserControl
    {
        public DatosPersonales()
        {
            InitializeComponent();
            lbSexo.SelectedIndex = 0;
        }

        public void GuardarModelo(PacienteDTO paciente)
        {
            paciente.Cedula = txtCedula.Text.Trim();
            paciente.Nombre = txtNombre.Text.Trim();
            paciente.Apellido = txtApellidos.Text.Trim();
            paciente.FechaNacimiento = dtpFecha.Value;
            paciente.Edad = txtEdad.Text;
            paciente.Genero = lbSexo.SelectedItem?.ToString() ?? "Femenino";
            paciente.Nacionalidad = txtNacionalidad.Text.Trim();
            paciente.Direccion = txtDireccion.Text.Trim();
            paciente.Ocupacion = txtOcupacion.Text.Trim();
            paciente.Telefono = txtNumeroTel.Text.Trim();

        }

        public void CargarModelo(PacienteDTO paciente)
        {
            txtCedula.Text = paciente.Cedula;
            txtNombre.Text = paciente.Nombre;
            txtApellidos.Text = paciente.Apellido;
            dtpFecha.Value = paciente.FechaNacimiento;
            txtEdad.Text = paciente.Edad;
            txtNacionalidad.Text = paciente.Nacionalidad;
            txtDireccion.Text = paciente.Direccion;
            txtOcupacion.Text = paciente.Ocupacion;
            txtNumeroTel.Text = paciente.Telefono;
            if (!string.IsNullOrEmpty(paciente.Genero))
            {
                lbSexo.SelectedItem = paciente.Genero;
            }
        }

        public bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtCedula.Text))
            {
                MessageBox.Show("La cédula es obligatoria", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("El apellido es obligatorio", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellidos.Focus();
                return false;
            }


            return true;
        }
    }
}
