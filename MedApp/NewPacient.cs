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
        private Paciente pacienteActual;
        private int pasoActual = 0;
        private List<UserControl> pasos;
        private PacienteService pacienteService;

        private DatosPersonales paso1;
        private AntecedentesMedicosControl paso2;

        public NewPacient()
        {
            InitializeComponent();
            InicializarFormulario();
            ConexionBD conexion = new ConexionBD();
            pacienteService = new PacienteService(conexion);
        }

        private void InicializarFormulario()
        {
            pacienteActual = new Paciente
            {
                FechaNacimiento = DateTime.Now.AddYears(-30),
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
            NextBtn.Visible = numeroPaso < pasos.Count - 1;
            SaveBtn.Visible = numeroPaso == pasos.Count - 1;

            pasoActual = numeroPaso;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar el registro?",
            "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                new Main().Show();
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
                        paso1.GuardarModelo(pacienteActual);
                    break;
                case 1:
                    esValido = paso2.ValidarDatos();
                    if (esValido)
                        paso2.GuardarModelo(pacienteActual);
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
                    paso1.GuardarModelo(pacienteActual);
                    break;
                case 2:
                    paso2.GuardarModelo(pacienteActual);
                    break;
            }

            MostrarPaso(pasoActual - 1);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Validar el último paso
            if (!paso2.ValidarDatos())
                return;

            // Guardar datos del último paso
            paso2.GuardarModelo(pacienteActual);

            // Aquí guardarías en la base de datos
            if (pacienteService.GuardarPaciente(pacienteActual))
            {
                MessageBox.Show("Paciente registrado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                
                if (MessageBox.Show("Desea abrir una consulta con este paciente?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NewConsult consult = new NewConsult(pacienteActual);
                    consult.ShowDialog();
                }
                else
                {
                    new Main().Show();
                }

                this.Close();
            }

        }
    }
}
