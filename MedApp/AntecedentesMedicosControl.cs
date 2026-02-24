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
    public partial class AntecedentesMedicosControl : UserControl
    {
        public AntecedentesMedicosControl()
        {
            InitializeComponent();
        }

        public void GuardarModelo(PacienteDTO paciente)
        {
            paciente.OperacionesPrevias = txtCirugias.Text;
            paciente.AntecedentesFamiliares = txtAntecedentesFamiliares.Text;
            paciente.AntecedentesPatologicos.Clear();
            foreach (var item in clbAntecedentesPatologicos.CheckedItems)
            {
                paciente.AntecedentesPatologicos.Add(item.ToString());
            }
        }

        public void CargarModelo(PacienteDTO paciente)
        {
            txtCirugias.Text = paciente.OperacionesPrevias;
            txtAntecedentesFamiliares.Text = paciente.AntecedentesFamiliares;
            for (int i = 0; i < clbAntecedentesPatologicos.Items.Count; i++)
            {
                string item = clbAntecedentesPatologicos.Items[i].ToString();
                if (paciente.AntecedentesPatologicos.Contains(item))
                {
                    clbAntecedentesPatologicos.SetItemChecked(i, true);
                }
            }
        }
        private void clbAntecedentesPatologicos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string itemSeleccionado = clbAntecedentesPatologicos.Items[e.Index].ToString();

            if (itemSeleccionado == "Ninguno" && e.NewValue == CheckState.Checked)
            {
                // Desmarcar todos los demás
                for (int i = 0; i < clbAntecedentesPatologicos.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        clbAntecedentesPatologicos.SetItemChecked(i, false);
                    }
                }
            }
            else if (e.NewValue == CheckState.Checked && itemSeleccionado != "Ninguno")
            {
                // Si marcan algo diferente a "Ninguno", desmarcar "Ninguno"
                int ningunoIndex = clbAntecedentesPatologicos.Items.IndexOf("Ninguno");
                if (ningunoIndex >= 0)
                {
                    clbAntecedentesPatologicos.SetItemChecked(ningunoIndex, false);
                }
            }
        }

        public bool ValidarDatos()
        {
            return true;
        }

        private void clbAntecedentesPatologicos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
