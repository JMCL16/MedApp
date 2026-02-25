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
    public partial class GestionUsuarios : Form
    {
        //Implementar la api de editar el rol
        private readonly UsuarioService _usuarioService;
        public GestionUsuarios()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
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
    }
}
