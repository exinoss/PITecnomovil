using PITecnomovil.Modelo;
using PITecnomovil.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PITecnomovil
{
    public partial class frmReparaciones : Form
    {
        private int? _selectedReparacionId;
        private bool _RegistrarActualizar;
        private readonly IReparacionService _reparacionService;
        private readonly IClienteService _clienteService;
        private CancellationTokenSource _searchClientesCts;
        private CancellationTokenSource _searchReparacionesCts;
        private List<Reparacion> _reparacionesCache;
        private List<Cliente> _clientesCache;

        // Esta variable contiene el Id del usuario que crea la reparación
        private readonly int _idUsuario;
        // Cuando se edita, guardamos aquí el IdUsuario que vino de la BD
        private int _loadedUsuarioId;
        public frmReparaciones(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            _RegistrarActualizar = true;
            _reparacionService = new ReparacionService();
            _clienteService = new ClienteService();


            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Visible = true;

            LoadClientes();
            LoadReparaciones();
            ResetForm();
        }
        private async void LoadClientes()
        {
            _clientesCache = await _clienteService.GetClientesAsync();
        }

        private async void LoadReparaciones()
        {
            try
            {
                _reparacionesCache = await _reparacionService.GetReparacionesAsync();
                var listaPlano = _reparacionesCache
                    .Select(r => new
                    {
                        r.IdReparacion,
                        Cliente = r.Cliente.Nombres,
                        Usuario = r.Usuario.NombreUsuario,
                        r.Estado,
                        FechaIngreso = r.FechaIngreso.ToShortDateString(),
                        FechaEntrega = r.FechaEntrega?.ToShortDateString() ?? "",
                        r.Dispositivo,
                        r.PrecioServicio
                    })
                    .ToList();

                dataGridViewALL.DataSource = listaPlano;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reparaciones: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            _selectedReparacionId = null;
            _loadedUsuarioId = 0;
            _RegistrarActualizar = true;
            btnGuardar.Text = "Guardar";

            txtBuscarCliente.Text = "";
            txtNombres.Text = "";
            txtDispositivo.Text = "";
            txtDiagnostico.Text = "";
            txtObservaciones.Text = "";
            txtPrecio.Text = "";
            cmbEstado.SelectedIndex = -1;
            dtFechaIngreso.Value = DateTime.Today;
            dtFechaEntrega.Value = DateTime.Today.AddDays(1);

            dataGridViewALL.ClearSelection();
        }
        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmReparaciones_Load(object sender, EventArgs e)
        {

        }
        private bool TryBuildReparacion(out Reparacion reparacion)
        {
            reparacion = null;

            MessageBox.Show(txtNombres.Tag.ToString());
            // Cliente
            if (!(txtNombres.Tag is int idCliente))
            {
                MessageBox.Show("Debes buscar y seleccionar un cliente.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Estado
            var estado = cmbEstado.Text.Trim();
            if (string.IsNullOrWhiteSpace(estado))
            {
                MessageBox.Show("Selecciona un estado.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Dispositivo
            var dispositivo = txtDispositivo.Text.Trim();
            if (string.IsNullOrWhiteSpace(dispositivo))
            {
                MessageBox.Show("Ingresa el dispositivo.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // PrecioServicio
            if (!decimal.TryParse(txtPrecio.Text.Trim(), out var precio) || precio < 0m)
            {
                MessageBox.Show("El precio del servicio debe ser un número válido.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Fechas
            var ingreso = dtFechaIngreso.Value.Date;
            DateTime? entrega = null;

            if (cmbEstado.Text == "Entregado")
            {
                // validar entrega sólo si está habilitado (es Entregado)
                entrega = dtFechaEntrega.Value.Date;
                if (entrega < ingreso.AddDays(1))
                {
                    MessageBox.Show(
                      "La fecha de entrega debe ser al menos un día posterior a la de ingreso.",
                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
                    return false;
                }
            }

            reparacion = new Reparacion
            {
                IdReparacion = _selectedReparacionId.GetValueOrDefault(),
                IdCliente = idCliente,
                IdUsuario = _RegistrarActualizar ? _idUsuario : _loadedUsuarioId,
                FechaIngreso = ingreso,
                FechaEntrega = entrega,
                Estado = estado,
                Dispositivo = dispositivo,
                PrecioServicio = precio,
                Diagnostico = txtDiagnostico.Text.Trim(),
                Observaciones = txtObservaciones.Text.Trim()
            };
            return true;
        }
        private void dtFechaIngreso_ValueChanged(object sender, EventArgs e)
        {
            // Fecha de entrega al menos un día después
            if (dtFechaEntrega.Enabled)
            {
                dtFechaEntrega.MinDate = dtFechaIngreso.Value.AddDays(1);
                if (dtFechaEntrega.Value < dtFechaEntrega.MinDate)
                    dtFechaEntrega.Value = dtFechaEntrega.MinDate;
            }
        }

        private void btnLimpiarCliente_Click(object sender, EventArgs e)
        {
            txtBuscarCliente.Text = "";
            txtNombres.Text = "";
        }

        private async void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            _searchClientesCts?.Cancel();
            _searchClientesCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(300, _searchClientesCts.Token);
                var filtro = txtBuscarCliente.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(filtro))
                {
                    // nada
                    return;
                }

                var resultados = _clientesCache
                    .Where(c =>
                        c.Cedula.ToLower().Contains(filtro) ||
                        c.Nombres.ToLower().Contains(filtro))
                    .ToList();

                if (resultados.Count == 1)
                {
                    var cliente = resultados[0];
                    txtNombres.Text = cliente.Nombres;
                    // Guardamos el IdCliente para el POST/PUT
                    _selectedReparacionId = null;
                    // almacenamos IdCliente en Tag para el build
                    txtNombres.Tag = cliente.IdCliente;
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cliente: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void txtBuscarReparacion_TextChanged(object sender, EventArgs e)
        {
            _searchReparacionesCts?.Cancel();
            _searchReparacionesCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(300, _searchReparacionesCts.Token);
                var filtro = txtBuscarReparacion.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(filtro))
                {
                    dataGridViewALL.DataSource = _reparacionesCache;
                }
                else
                {
                    var filtradas = _reparacionesCache
                        .Where(r =>
                            r.Cliente.Cedula.ToLower().Contains(filtro) ||
                            r.Cliente.Nombres.ToLower().Contains(filtro) ||
                            r.Usuario.NombreUsuario.ToLower().Contains(filtro))
                        .ToList();
                    dataGridViewALL.DataSource = filtradas;
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar reparación: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void dataGridViewALL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

         
            try
            {
                var repar = dataGridViewALL.Rows[e.RowIndex].DataBoundItem as Reparacion;
                if (repar == null) return;

                // 2) Guarda el Id correcto
                _selectedReparacionId = repar.IdReparacion;
                _loadedUsuarioId = repar.IdUsuario;
                MessageBox.Show("user: "+_loadedUsuarioId+" "+"rep: "+_selectedReparacionId);
                var reparacion = await _reparacionService.GetReparacionAsync((int)_selectedReparacionId);
                // Cargo campos
               
                txtBuscarCliente.Text = reparacion.Cliente.Cedula;
                txtNombres.Text = reparacion.Cliente.Nombres;
                txtNombres.Tag = reparacion.IdCliente;
                cmbEstado.Text = reparacion.Estado;
                txtDispositivo.Text = reparacion.Dispositivo;
                txtDiagnostico.Text = reparacion.Diagnostico;
                txtObservaciones.Text = reparacion.Observaciones;
                txtPrecio.Text = reparacion.PrecioServicio.ToString();
                dtFechaIngreso.Value = reparacion.FechaIngreso;
                if (reparacion.FechaEntrega.HasValue)
                    dtFechaEntrega.Value = reparacion.FechaEntrega.Value;

                _RegistrarActualizar = false;
                btnGuardar.Text = "Actualizar";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reparación: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TryBuildReparacion(out var rep))
                    return;

                var msj = _RegistrarActualizar
                    ? "¿Confirma creación de la reparación?"
                    : "¿Confirma actualización de la reparación?";
                if (!MessageBox.Show(msj, "Confirmación",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question)
                      .Equals(DialogResult.Yes))
                    return;

                if (_RegistrarActualizar)
                    await _reparacionService.AddReparacionAsync(rep);
                else
                    await _reparacionService.UpdateReparacionAsync(rep.IdReparacion, rep);

                LoadReparaciones();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al {(_RegistrarActualizar ? "guardar" : "actualizar")}: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_selectedReparacionId.HasValue)
                {
                    MessageBox.Show("Selecciona una reparación para eliminar.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("¿Confirma eliminación de la reparación?", "Confirmación",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                    return;

                await _reparacionService.DeleteReparacionAsync(_selectedReparacionId.Value);
                LoadReparaciones();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            var estado = cmbEstado.Text;
            if (estado == "Entregado")
            {
                // Entregado, fecha obligatoria y con mínimo de 1 día
                dtFechaEntrega.Enabled = true;
                dtFechaIngreso_ValueChanged(this, EventArgs.Empty);
            }
            else
            {
                // Pendiente o Cancelado, no obligatorio, desactivado
                dtFechaEntrega.Enabled = false;
            }
        }
    }
}

