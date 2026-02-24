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

        public DateTime FechaSeleccionada
        {
            get { return dtpFecha.Value; }
        }
        public string Genero
        {
            get { return lbSexo.SelectedItem?.ToString() ?? "Femenino"; }
        }
        public string Cedula
        {
            get { return txtCedula.Text.Trim(); }
        }
        public string Nombre
        {
            get { return txtNombre.Text.Trim(); }
        }
        public string Apellido
        {
            get { return txtApellidos.Text.Trim(); }
        }
        public string Nacionalidad
        {
            get { return txtNacionalidad.Text.Trim(); }
        }
        public string Direccion
        {
            get { return txtDireccion.Text.Trim(); }
        }
        public string Ocupacion
        {
            get { return txtOcupacion.Text.Trim(); }
        }
        public string Telefono
        {
            get { return txtNumeroTel.Text.Trim(); }
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
            List<string> errores = new List<string>();
            if (string.IsNullOrWhiteSpace(txtCedula.Text))
            {
                errores.Add("La cédula es obligatoria");
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errores.Add("El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                errores.Add("El apellido es obligatorio");
            }
            if(Telefono.Length != 10 || !Telefono.All(char.IsDigit))
            {
                errores.Add("El teléfono debe tener 10 dígitos numéricos.");
            }
            DateTime fechaNacimiento = dtpFecha.Value;
            int edadCalculada = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edadCalculada))
                edadCalculada--;
            if (edadCalculada < 0)
            {
                errores.Add("La fecha de nacimiento no puede ser una futura");
            }

            if (errores.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errores), "Corrija los siguientes errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
