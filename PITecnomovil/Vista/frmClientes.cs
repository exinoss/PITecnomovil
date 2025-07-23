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
    public partial class frmClientes : Form
    {
        private int? _selectedClientId;
        private bool _RegistrarActualizar;
        private readonly IClienteService _clienteService;
        private CancellationTokenSource _searchCancellationTokenSource;
        private List<Cliente> _clientesCache;
        public frmClientes()
        {
            InitializeComponent(); 
            _clienteService = new ClienteService();
            _RegistrarActualizar = true;

            // Para integrarlo en tu panel/MDI
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Visible = true;

            LoadClientes();
        }
        private async void LoadClientes()
        {
            try
            {
                _clientesCache = await _clienteService.GetClientesAsync();
                dataGridViewALL.DataSource = _clientesCache;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clean()
        {
            txtNombres.Text = "";
            txtCedula.Text = "";
            txtContacto.Text = "";

            _selectedClientId = null;
            _RegistrarActualizar = true;
            btnGuardar.Text = "Guardar";

            LoadClientes();
        }
        private void dataGridViewALL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridViewALL.Rows[e.RowIndex];

            _selectedClientId = (int)row.Cells["IdCliente"].Value;

            txtNombres.Text = row.Cells["Nombres"].Value?.ToString() ?? "";
            txtCedula.Text = row.Cells["Cedula"].Value?.ToString() ?? "";
            txtContacto.Text = row.Cells["Contacto"].Value?.ToString() ?? "";

            _RegistrarActualizar = false;
            btnGuardar.Text = "Actualizar";
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TryBuildCliente(out var cliente))
                    return;

                var mensaje = _RegistrarActualizar
                    ? "¿Estás seguro de que deseas guardar este cliente?"
                    : "¿Estás seguro de que deseas actualizar este cliente?";

                if (!ConfirmarAccion(mensaje))
                    return;

                if (_RegistrarActualizar)
                    await _clienteService.AddClienteAsync(cliente);
                else
                    await _clienteService.UpdateClienteAsync(cliente.IdCliente, cliente);

                Clean();
            }
            catch (InvalidOperationException ex)
            {
                // conflicto 409 de cédula duplicada
                MessageBox.Show(ex.Message, "Error de validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                var acción = _RegistrarActualizar ? "Registrar" : "Actualizar";
                MessageBox.Show($"Error al {acción}: {ex.Message}",
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

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_selectedClientId.HasValue)
                {
                    MessageBox.Show("Selecciona un cliente para eliminar.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ConfirmarAccion("¿Estás seguro de eliminar este cliente?"))
                    return;

                await _clienteService.DeleteClienteAsync(_selectedClientId.Value);
                Clean();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    dataGridViewALL.DataSource = _clientesCache;
                else
                    dataGridViewALL.DataSource = _clientesCache
                        .Where(c =>
                            c.Nombres.ToLower().Contains(filtro) ||
                            c.Cedula.ToLower().Contains(filtro))
                        .ToList();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool TryBuildCliente(out Cliente cliente)
        {
            cliente = null;

            // 1) Nombres
            var nombres = txtNombres.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombres))
            {
                MessageBox.Show("El campo 'Nombres' no puede estar vacío.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 2) Cédula
            var cedula = txtCedula.Text.Trim();
            if (string.IsNullOrWhiteSpace(cedula))
            {
                MessageBox.Show("El campo 'Cédula' no puede estar vacío.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!cedula.All(char.IsDigit))
            {
                MessageBox.Show("El campo 'Cédula' solo puede contener números.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 3) Contacto (opcional)
            var contacto = txtContacto.Text.Trim();

            cliente = new Cliente
            {
                IdCliente = _selectedClientId.GetValueOrDefault(),
                Nombres = nombres,
                Cedula = cedula,
                Contacto = contacto
            };

            return true;
        }
        private void frmClientes_Load(object sender, EventArgs e)
        {

        }
    }
}
