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
    public partial class NewPacient : Form
    {
        private readonly PacienteService _service;
        private PacienteDTO pacienteDto;
        private int pasoActual = 0;
        private List<UserControl> pasos;

        private DatosPersonales paso1;
        private AntecedentesMedicosControl paso2;

        public NewPacient()
        {
            InitializeComponent();
            InicializarFormulario();
            _service = new PacienteService();
        }

        private void InicializarFormulario()
        {
            pacienteDto = new PacienteDTO
            {
                FechaNacimiento = DateTime.Now.AddYears(-30),
                AntecedentesPatologicos = new List<string>()
            };

            //Crear intancias de los userControls
            paso1 = new DatosPersonales { Dock = DockStyle.Fill };
            paso2 = new AntecedentesMedicosControl { Dock = DockStyle.Fill };
            pasos = new List<UserControl> { paso1, paso2 };
            MostrarPaso(0);
        }

        private void MostrarPaso(int numeroPaso)
        {
            // Limpiar el panel contenedor
            panelContenedor.Controls.Clear();

            // Agregar el UserControl correspondiente
            panelContenedor.Controls.Add(pasos[numeroPaso]);

            // Actualizar etiqueta de progreso
            lblPaso.Text = $"Paso {numeroPaso + 1} de {pasos.Count}";

            // Actualizar estado de botones
            BackBtn.Enabled = numeroPaso > 0;
            if (numeroPaso < pasos.Count - 1)
            {
                NextBtn.Visible = true;
                SaveBtn.Visible = false;
            }
            else
            {
                NextBtn.Visible = false;
                SaveBtn.Visible = true;
            }

            pasoActual = numeroPaso;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar el registro?",
            "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            bool esValido = false;

            switch (pasoActual)
            {
                case 0:
                    esValido = paso1.ValidarDatos();
                    if (esValido)
                        paso1.GuardarModelo(pacienteDto);
                    break;
                case 1:
                    esValido = paso2.ValidarDatos();
                    if (esValido)
                        paso2.GuardarModelo(pacienteDto);
                    break;
            }

            if (esValido)
            {
                MostrarPaso(pasoActual + 1);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            switch (pasoActual)
            {
                case 1:
                    paso1.GuardarModelo(pacienteDto);
                    break;
                case 2:
                    paso2.GuardarModelo(pacienteDto);
                    break;
            }

            MostrarPaso(pasoActual - 1);
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            // Validar el último paso
            if (!paso2.ValidarDatos())
                return;

            // Guardar datos del último paso
            paso2.GuardarModelo(pacienteDto);

            SaveBtn.Enabled = false;
            SaveBtn.Text = "Guardando...";
            Cursor = Cursors.WaitCursor;

            try
            {
                DateTime fechaNac = paso1.FechaSeleccionada;
                int edadCalculada = DateTime.Today.Year - fechaNac.Year;
                if (fechaNac.Date > DateTime.Today.AddYears(-edadCalculada)) edadCalculada--;

                var pacienteDto = new PacienteDTO
                {
                    Cedula = paso1.Cedula,
                    Nombre = paso1.Nombre,
                    Apellido = paso1.Apellido,
                    FechaNacimiento = fechaNac,
                    Edad = edadCalculada.ToString(),
                    Genero = paso1.Genero,
                    Nacionalidad = paso1.Nacionalidad,
                    Direccion = paso1.Direccion,
                    Ocupacion = paso1.Ocupacion,
                    Telefono = paso1.Telefono,
                    OperacionesPrevias = paso2.OperacionesPrevias,
                    AntecedentesFamiliares = paso2.AntecedentesFamiliares,
                    AntecedentesPatologicos = paso2.AntecedentesPatologicos,
                    UsuarioRegistro = Sesion.IdUsuario,
                    FechaRegistro = DateTime.Now,
                };

                var resultado = await _service.CrearPaciente(pacienteDto);

                if (resultado != null && resultado.IsSuccess)
                {
                    MessageBox.Show("Paciente creado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    
                    if( MessageBox.Show("¿Desea registrar una consulta para este paciente?", "Registrar Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var pacienteCreado = await _service.ObtenerPacientePorCedula(pacienteDto.Cedula);
                        var consult = new NewConsult(pacienteCreado);  
                        consult.ShowDialog();
                    }
                    this.Close();
                }
                else
                {
                    string mensajeError = resultado != null ? resultado.Message : "Ocurrió un error desconocido.";
                    MessageBox.Show(mensajeError, "Corrija los siguientes errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexion {ex.Message}");
            }
            finally
            {
                SaveBtn.Enabled = true;
                SaveBtn.Text = "Guardar";
                Cursor = Cursors.Default;
            }

        }

        private void NewPacient_Load(object sender, EventArgs e)
        {

        }
    }
}
