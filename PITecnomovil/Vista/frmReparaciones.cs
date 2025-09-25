using PITecnomovil.Modelo;
using PITecnomovil.Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
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
     
        // Variables de usuario
        private readonly int _idUsuario; // Id del usuario que crea la reparación
        private int _loadedUsuarioId; // Id del usuario cuando se edita una reparación existente

        // Textos de ayuda para placeholders
        private const string PLACEHOLDER_DIAGNOSTICO = "Ingrese el diagnóstico del dispositivo...";
        private const string PLACEHOLDER_OBSERVACIONES = "Ingrese observaciones adicionales...";
        // Esta variable contiene el Id del usuario que crea la reparación
 
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

            ConfigurarPlaceholders();
            ResetForm();
            _ = LoadClientesAsync(); // Fire-and-forget para carga inicial
            _ = LoadReparacionesAsync(); // Fire-and-forget para carga inicial
        }
        private async Task LoadClientesAsync()
        {
            try
            {
                _clientesCache = await _clienteService.GetClientesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarPlaceholders()
        {
            // Configurar placeholder para txtDiagnostico
            SetPlaceholder(txtDiagnostico, PLACEHOLDER_DIAGNOSTICO);
            txtDiagnostico.Enter += TxtDiagnostico_Enter;
            txtDiagnostico.Leave += TxtDiagnostico_Leave;

            // Configurar placeholder para txtObservaciones
            SetPlaceholder(txtObservaciones, PLACEHOLDER_OBSERVACIONES);
            txtObservaciones.Enter += TxtObservaciones_Enter;
            txtObservaciones.Leave += TxtObservaciones_Leave;
        }

        private void SetPlaceholder(MaterialSkin.Controls.MaterialMultiLineTextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = System.Drawing.Color.Gray;
        }

        private void RemovePlaceholder(MaterialSkin.Controls.MaterialMultiLineTextBox textBox, string placeholder)
        {
            if (textBox.Text == placeholder)
            {
                textBox.Text = "";
                textBox.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void RestorePlaceholder(MaterialSkin.Controls.MaterialMultiLineTextBox textBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                SetPlaceholder(textBox, placeholder);
            }
        }

        private bool IsPlaceholder(MaterialSkin.Controls.MaterialMultiLineTextBox textBox, string placeholder)
        {
            return textBox.Text == placeholder && textBox.ForeColor == System.Drawing.Color.Gray;
        }

        private async Task LoadReparacionesAsync()
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
            txtNombres.Tag = null;
            txtDispositivo.Text = "";
            txtPrecio.Text = "";
            cmbEstado.SelectedIndex = -1;
            dtFechaIngreso.Value = DateTime.Today;
            dtFechaEntrega.Value = DateTime.Today.AddDays(1);

            // Restaurar placeholders
            SetPlaceholder(txtDiagnostico, PLACEHOLDER_DIAGNOSTICO);
            SetPlaceholder(txtObservaciones, PLACEHOLDER_OBSERVACIONES);

            dataGridViewALL.ClearSelection();
        }
        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            // Permitir solo números y punto decimal
            string text = txtPrecio.Text;
            if (!string.IsNullOrEmpty(text))
            {
                // Remover caracteres no válidos excepto números y un punto decimal
                string cleanedText = "";
                bool hasDecimalPoint = false;
                
                foreach (char c in text)
                {
                    if (char.IsDigit(c))
                    {
                        cleanedText += c;
                    }
                    else if (c == '.' && !hasDecimalPoint)
                    {
                        cleanedText += c;
                        hasDecimalPoint = true;
                    }
                }
                
                if (cleanedText != text)
                {
                    txtPrecio.Text = cleanedText;
                    txtPrecio.SelectionStart = txtPrecio.Text.Length;
                }
            }
        }

        private void frmReparaciones_Load(object sender, EventArgs e)
        {

        }
        private bool TryBuildReparacion(out Reparacion reparacion)
        {
            reparacion = null;

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
            if (!decimal.TryParse(txtPrecio.Text.Trim(), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out var precio) || precio < 0m)
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

            // Obtener diagnóstico y observaciones sin placeholders
            string diagnostico = IsPlaceholder(txtDiagnostico, PLACEHOLDER_DIAGNOSTICO) 
                ? "" 
                : txtDiagnostico.Text.Trim();
            
            string observaciones = IsPlaceholder(txtObservaciones, PLACEHOLDER_OBSERVACIONES) 
                ? "" 
                : txtObservaciones.Text.Trim();

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
                Diagnostico = diagnostico,
                Observaciones = observaciones
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
            txtNombres.Tag = null;
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

                if (_clientesCache == null || !_clientesCache.Any())
                {
                    return; // Cache no disponible aún
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

                if (_reparacionesCache == null || !_reparacionesCache.Any())
                {
                    return; // Cache no disponible aún
                }

                List<Reparacion> reparacionesAMostrar;

                if (string.IsNullOrEmpty(filtro))
                {
                    reparacionesAMostrar = _reparacionesCache;
                }
                else
                {
                    reparacionesAMostrar = _reparacionesCache
                        .Where(r =>
                            r.Cliente.Cedula.ToLower().Contains(filtro) ||
                            r.Cliente.Nombres.ToLower().Contains(filtro) ||
                            r.Usuario.NombreUsuario.ToLower().Contains(filtro))
                        .ToList();
                }

                // Crear la proyección consistente con LoadReparaciones()
                var listaPlano = reparacionesAMostrar
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
                // Obtener el IdReparacion de la fila seleccionada
                var row = dataGridViewALL.Rows[e.RowIndex];
                if (row.Cells["IdReparacion"].Value == null) return;

                int idReparacion = Convert.ToInt32(row.Cells["IdReparacion"].Value);
                
                // Buscar la reparación completa en el cache
                var reparacionCache = _reparacionesCache.FirstOrDefault(r => r.IdReparacion == idReparacion);
                if (reparacionCache == null)
                {
                    MessageBox.Show("No se pudo encontrar la reparación seleccionada.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Guardar IDs para operaciones posteriores
                _selectedReparacionId = idReparacion;
                _loadedUsuarioId = reparacionCache.IdUsuario;

                // Obtener los detalles completos de la reparación desde la API
                var reparacion = await _reparacionService.GetReparacionAsync(idReparacion);
                
                // Cargar campos
               
                txtBuscarCliente.Text = reparacion.Cliente.Cedula;
                txtNombres.Text = reparacion.Cliente.Nombres;
                txtNombres.Tag = reparacion.IdCliente;
                cmbEstado.Text = reparacion.Estado;
                txtDispositivo.Text = reparacion.Dispositivo;
                txtPrecio.Text = reparacion.PrecioServicio.ToString("0.00", CultureInfo.InvariantCulture);
                dtFechaIngreso.Value = reparacion.FechaIngreso;
                if (reparacion.FechaEntrega.HasValue)
                    dtFechaEntrega.Value = reparacion.FechaEntrega.Value;

                // Manejar diagnóstico con placeholder
                if (!string.IsNullOrWhiteSpace(reparacion.Diagnostico))
                {
                    txtDiagnostico.Text = reparacion.Diagnostico;
                    txtDiagnostico.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    SetPlaceholder(txtDiagnostico, PLACEHOLDER_DIAGNOSTICO);
                }

                // Manejar observaciones con placeholder
                if (!string.IsNullOrWhiteSpace(reparacion.Observaciones))
                {
                    txtObservaciones.Text = reparacion.Observaciones;
                    txtObservaciones.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    SetPlaceholder(txtObservaciones, PLACEHOLDER_OBSERVACIONES);
                }

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

                await LoadReparacionesAsync();
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
                await LoadReparacionesAsync();
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        // Eventos para placeholder de txtDiagnostico
        private void TxtDiagnostico_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(txtDiagnostico, PLACEHOLDER_DIAGNOSTICO);
        }

        private void TxtDiagnostico_Leave(object sender, EventArgs e)
        {
            RestorePlaceholder(txtDiagnostico, PLACEHOLDER_DIAGNOSTICO);
        }

        // Eventos para placeholder de txtObservaciones
        private void TxtObservaciones_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(txtObservaciones, PLACEHOLDER_OBSERVACIONES);
        }

        private void TxtObservaciones_Leave(object sender, EventArgs e)
        {
            RestorePlaceholder(txtObservaciones, PLACEHOLDER_OBSERVACIONES);
        }
    }
}

