using MedApp.DTOs.User;
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
    public partial class NewUsuario : Form
    {
        private readonly UsuarioService _service;
        //Completar este form
        //Crear la Api de nuevo usuario
        public NewUsuario()
        {
            InitializeComponent();
            _service = new UsuarioService();
        }

        private async void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;
                var usuarioDto = new CrearUsuarioDTO
                {
                    UserName = txtUsuario.Text.Trim(),
                    Password = txtContrasena.Text,
                    Roles = cmbRol.SelectedItem.ToString()
                };

                

                var usuario = await _service.CrearUsuario(usuarioDto);
                if (usuario != null && usuario.IsSuccess)
                {
                    MessageBox.Show("Usuario creado exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string mensajeError = usuario != null ? usuario.Message : "Ocurrió un error desconocido.";
                    MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Ingrese el nombre de usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Ingrese la contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Focus();
                return false;
            }
            if (txtContrasena.Text != txtConfirmContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmContrasena.Focus();
                return false;
            }
            return true;
        }

    }
}
