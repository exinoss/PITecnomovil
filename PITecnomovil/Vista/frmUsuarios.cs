using PITecnomovil.Modelo;
using PITecnomovil.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PITecnomovil.Vista
{
    public partial class frmUsuarios : Form
    {
        private int? _selectedUserId;
        private bool _RegistrarActualizar;
        private readonly IUsuarioService _usuarioService;
        private CancellationTokenSource _searchCancellationTokenSource;
        private List<Usuario> _usuariosCache;
        public frmUsuarios()
        {
            InitializeComponent(); 
            _usuarioService = new UsuarioService();
            _RegistrarActualizar = true;

            // Configuración para integrarse en tu contenedor MDI o panel
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Visible = true;

            LoadUsuarios();
        }
        private void Clean()
        {
            txtNombreUsuario.Text = "";
            txtContrasenia.Text = "";
            cmbRol.SelectedIndex = -1;

            _selectedUserId = null;
            _RegistrarActualizar = true;
            btnGuardar.Text = "Guardar";

            LoadUsuarios();
        }
        private async void LoadUsuarios()
        {
            try
            {
                _usuariosCache = await _usuarioService.GetUsuariosAsync();
                dataGridViewALL.DataSource = _usuariosCache;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewALL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridViewALL.Rows[e.RowIndex];
            _selectedUserId = (int)row.Cells["IdUsuario"].Value;

            txtNombreUsuario.Text = row.Cells["NombreUsuario"].Value?.ToString() ?? "";
            txtContrasenia.Text = row.Cells["Clave"].Value?.ToString() ?? "";
            cmbRol.Text = row.Cells["Rol"].Value?.ToString() ?? "";

            _RegistrarActualizar = false;
            btnGuardar.Text = "Actualizar";
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TryBuildUsuario(out var usuario))
                    return;

                var mensaje = _RegistrarActualizar
                    ? "¿Estás seguro de que deseas guardar este usuario?"
                    : "¿Estás seguro de que deseas actualizar este usuario?";

                if (!ConfirmarAccion(mensaje))
                    return;

                if (_RegistrarActualizar)
                    await _usuarioService.AddUsuarioAsync(usuario);
                else
                    await _usuarioService.UpdateUsuarioAsync(usuario.IdUsuario, usuario);

                Clean();
            }
            catch (Exception ex)
            {
                var acción = _RegistrarActualizar ? "Registrar" : "Actualizar";
                MessageBox.Show($"Error al {acción}: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_selectedUserId.HasValue)
                {
                    MessageBox.Show("Selecciona un usuario para eliminar.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ConfirmarAccion("¿Estás seguro de eliminar este usuario?"))
                    return;

                await _usuarioService.DeleteUsuarioAsync(_selectedUserId.Value);
                Clean();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ConfirmarAccion(string mensaje)
        {
            return MessageBox.Show(mensaje,
                                   "Confirmación",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question)
                   == DialogResult.Yes;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContrasenia_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _searchCancellationTokenSource?.Cancel();
            _searchCancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(300, _searchCancellationTokenSource.Token);
                var filtro = txtBuscar.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(filtro))
                    dataGridViewALL.DataSource = _usuariosCache;
                else
                    dataGridViewALL.DataSource = _usuariosCache
                        .Where(u => u.NombreUsuario.ToLower().Contains(filtro)
                                 || u.Rol.ToLower().Contains(filtro))
                        .ToList();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool TryBuildUsuario(out Usuario usuario)
        {
            usuario = null;

            // 1) Nombre de usuario
            var nombre = txtNombreUsuario.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre de usuario no puede estar vacío.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 2) Contraseña
            var clave = txtContrasenia.Text.Trim();
            if (string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show("La contraseña no puede estar vacía.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 3) Rol
            var rol = cmbRol.Text.Trim();
            if (string.IsNullOrWhiteSpace(rol))
            {
                MessageBox.Show("Debes seleccionar un rol.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            usuario = new Usuario
            {
                IdUsuario = _selectedUserId.GetValueOrDefault(),
                NombreUsuario = nombre,
                Clave = clave,
                Rol = rol
            };

            return true;
        }
        private void frmUsuarios_Load(object sender, EventArgs e)
        {

        }
    }
}
