using MedApp.DTOs.User;
using MedApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedApp
{
    public partial class GestionUsuarios : Form
    {
        public UsuarioDTO UsuarioSeleccionado { get; private set; }
        //Implementar la api de editar el rol
        private readonly UsuarioService _usuarioService;
        public GestionUsuarios()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
            dgvUsuarios.AutoGenerateColumns = false;
        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            NewUsuario newUsuario = new NewUsuario();
            if (newUsuario.ShowDialog() == DialogResult.OK)
            {
                GestionUsuarios_Load(sender, e);
            }
        }

        private async void GestionUsuarios_Load(object sender, EventArgs e)
        {
            //Crear api de obtener todos los usuarios
            var usuarios = await _usuarioService.ObtenerTodos();
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarios;
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para editar.");
                return;
            }
            UsuarioSeleccionado = (UsuarioDTO)dgvUsuarios.CurrentRow?.DataBoundItem;
            var usuarioAct = new ActualizarRolDTO
            {
                IdUsuario = UsuarioSeleccionado.Id,
                NuevoRol = UsuarioSeleccionado.Roles
            };
            var resultado = await _usuarioService.ActualizarRol(usuarioAct);
            if (resultado != null && resultado.IsSuccess)
            {
                MessageBox.Show("Rol actualizado correctamente.");
                GestionUsuarios_Load(sender, e);
            }
            else
            {
                string errorMsg = resultado != null ? resultado.Message : "No se recibió respuesta del servidor.";
                MessageBox.Show(errorMsg, "Error al actualizar el rol.");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
