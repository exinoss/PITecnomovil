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
    public partial class frmVentas : Form
    {
        private readonly int _idUsuario;
        private readonly IClienteService _clienteService;
        private readonly IReparacionService _reparacionService;
        private readonly IProductoService _productoService;
        private readonly IVentaService _ventaService;
        private readonly IVentaProductoService _ventaProductoService;
        private readonly IVentaReparacionService _ventaReparacionService;
        private readonly IFacturaService _facturaService;
        private readonly IPagoService _pagoService;
        private CancellationTokenSource _searchClientesCts;
        private List<Cliente> _clientesCache;
        private List<Reparacion> _unpaidRepairsCache;
        private List<Producto> _productosCache;

        public frmVentas(int idUsuario)
        {
            InitializeComponent();
           
            _idUsuario = idUsuario;
            _clienteService = new ClienteService();
            _reparacionService = new ReparacionService();
            _productoService = new ProductoService();
            _ventaService = new VentaService();
            _ventaProductoService = new VentaProductoService();
            _ventaReparacionService = new VentaReparacionService();
            _facturaService = new FacturaService();
            _pagoService = new PagoService();
            
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Visible = true;

            _ = LoadClientes(); // Fire-and-forget para carga inicial
            _ = LoadProductos(); // Fire-and-forget para carga inicial
            InitializeDataGridView();
            CalculateTotals(); // Inicializar totales en $0.00
        }

        private async Task LoadClientes()
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

        private async Task LoadProductos()
        {
            try
            {
                _productosCache = await _productoService.GetProductosAsync();
                
                // Debug: Verificar si se cargaron productos
                if (_productosCache == null)
                {
                    MessageBox.Show("No se pudieron cargar los productos (cache null).",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_productosCache.Count == 0)
                {
                    MessageBox.Show("No hay productos disponibles en la base de datos.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Debug: Mostrar cuántos productos se cargaron
                    Console.WriteLine($"Productos cargados: {_productosCache.Count}");
                }
                
                PopulateProductsCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task RefreshProductos()
        {
            await LoadProductos();
        }

        public async Task RefreshReparacionesCliente()
        {
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                var selectedClient = _clientesCache?.FirstOrDefault(c => 
                    c.Nombres.Equals(txtNombres.Text, StringComparison.OrdinalIgnoreCase));
                
                if (selectedClient != null)
                {
                    await LoadUnpaidRepairsByClient(selectedClient.IdCliente);
                }
            }
        }

        protected override async void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // Refrescar productos cada vez que el formulario recibe el foco
            await LoadProductos();
            
            // Si hay un cliente seleccionado, refrescar sus reparaciones también
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                var selectedClient = _clientesCache?.FirstOrDefault(c => 
                    c.Nombres.Equals(txtNombres.Text, StringComparison.OrdinalIgnoreCase));
                
                if (selectedClient != null)
                {
                    await LoadUnpaidRepairsByClient(selectedClient.IdCliente);
                }
            }
        }

        private void PopulateProductsCombo()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(PopulateProductsCombo));
                return;
            }

            try
            {
                cmbProductos.Items.Clear();
                if (_productosCache != null && _productosCache.Any())
                {
                    foreach (var producto in _productosCache)
                    {
                        cmbProductos.Items.Add(new ComboBoxItem
                        {
                            Text = $"ID: {producto.IdProducto} - {producto.Nombre} - ${producto.Precio:F2} (Stock: {producto.Stock})",
                            Value = producto
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al poblar combo de productos: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDataGridView()
        {
            dataGridViewALL.Columns.Clear();
            dataGridViewALL.AutoGenerateColumns = false;

            // Columna para tipo de item (Reparación o Producto)
            dataGridViewALL.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                Width = 100,
                ReadOnly = true
            });

            // Columna para ID
            dataGridViewALL.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                Width = 60,
                ReadOnly = true
            });

            // Columna para descripción/nombre
            dataGridViewALL.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                Width = 200,
                ReadOnly = true
            });

            // Columna para cantidad
            dataGridViewALL.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                Width = 80,
                ReadOnly = true
            });

            // Columna para precio unitario
            dataGridViewALL.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio Unit.",
                Width = 100,
                ReadOnly = true
            });

            // Columna para subtotal
            dataGridViewALL.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                Width = 100,
                ReadOnly = true
            });

            // Columna oculta para almacenar el objeto original
            dataGridViewALL.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ObjectData",
                HeaderText = "Data",
                Visible = false
            });
        }

        private async Task LoadUnpaidRepairsByClient(int idCliente)
        {
            try
            {
                _unpaidRepairsCache = await _reparacionService.GetUnpaidRepairsByClientAsync(idCliente);
                PopulateRepairsCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reparaciones pendientes: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateRepairsCombo()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(PopulateRepairsCombo));
                return;
            }

            try
            {
                cmbReparaciones.Items.Clear();
                if (_unpaidRepairsCache != null && _unpaidRepairsCache.Any())
                {
                    foreach (var repair in _unpaidRepairsCache)
                    {
                        cmbReparaciones.Items.Add(new ComboBoxItem
                        {
                            Text = $"ID: {repair.IdReparacion} - {repair.Dispositivo} - ${repair.PrecioServicio:F2}",
                            Value = repair
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al poblar combo de reparaciones: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clase auxiliar para el ComboBox
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void cmbReparaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Este evento no debe actualizar los totales
            // Los totales solo deben reflejar los ítems agregados al DataGridView
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void frmVentas_Load(object sender, EventArgs e)
        {
            // Cargar productos cuando el formulario esté completamente inicializado
            await LoadProductos();
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
                    txtNombres.Text = "";
                    txtNombres.Tag = null;
                    cmbReparaciones.Items.Clear();
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
                    txtNombres.Tag = cliente.IdCliente;
                    
                    // Cargar reparaciones pendientes del cliente
                    await LoadUnpaidRepairsByClient(cliente.IdCliente);
                }
                else
                {
                    txtNombres.Text = "";
                    txtNombres.Tag = null;
                    cmbReparaciones.Items.Clear();
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cliente: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewALL_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLimpiarCliente_Click(object sender, EventArgs e)
        {
            // Confirmar acción si hay datos en el formulario
            bool hasData = !string.IsNullOrEmpty(txtBuscarCliente.Text) || 
                          !string.IsNullOrEmpty(txtNombres.Text) || 
                          dataGridViewALL.Rows.Count > 0;

            if (hasData)
            {
                var result = MessageBox.Show("¿Está seguro de que desea limpiar todos los datos del cliente y la venta actual?", 
                                           "Confirmar limpieza", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // Limpiar todos los campos y controles
            txtBuscarCliente.Text = "";
            txtNombres.Text = "";
            txtNombres.Tag = null;
            cmbReparaciones.Items.Clear();
            cmbReparaciones.SelectedIndex = -1;
            cmbProductos.SelectedIndex = -1;
            txtCantidad.Text = "";
            
            // Limpiar DataGridView
            dataGridViewALL.Rows.Clear();
            
            // Resetear totales
            txtSubtotal.Text = "$0.00";
            txtIva.Text = "$0.00";
            txtTotal.Text = "$0.00";
        }

        private void btnQuitarRegistro_Click(object sender, EventArgs e)
        {
            if (dataGridViewALL.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("¿Está seguro de que desea eliminar el elemento seleccionado?", 
                                           "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    dataGridViewALL.Rows.RemoveAt(dataGridViewALL.SelectedRows[0].Index);
                    CalculateTotals();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un elemento para eliminar.", "Validación", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddReparaciones_Click(object sender, EventArgs e)
        {
            // Validar que hay un cliente seleccionado
            if (txtNombres.Tag == null || string.IsNullOrEmpty(txtNombres.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente antes de agregar reparaciones.", "Validación", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbReparaciones.SelectedItem is ComboBoxItem selectedItem && selectedItem.Value is Reparacion reparacion)
            {
                // Validar que no esté ya agregada
                foreach (DataGridViewRow row in dataGridViewALL.Rows)
                {
                    if (row.Cells["Tipo"].Value?.ToString() == "Reparación" && 
                        row.Cells["Id"].Value?.ToString() == reparacion.IdReparacion.ToString())
                    {
                        MessageBox.Show("Esta reparación ya ha sido agregada.", "Validación", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Agregar la reparación al DataGridView
                int rowIndex = dataGridViewALL.Rows.Add();
                DataGridViewRow newRow = dataGridViewALL.Rows[rowIndex];

                newRow.Cells["Tipo"].Value = "Reparación";
                newRow.Cells["Id"].Value = reparacion.IdReparacion;
                newRow.Cells["Descripcion"].Value = reparacion.Dispositivo;
                newRow.Cells["Cantidad"].Value = 1;
                newRow.Cells["PrecioUnitario"].Value = $"${reparacion.PrecioServicio:F2}";
                newRow.Cells["Subtotal"].Value = $"${reparacion.PrecioServicio:F2}";
                newRow.Cells["ObjectData"].Value = reparacion;

                // Recalcular totales
                CalculateTotals();

                // Limpiar selección
                cmbReparaciones.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Por favor seleccione una reparación.", "Validación", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddProductos_Click(object sender, EventArgs e)
        {
            // Validar que hay un cliente seleccionado
            if (txtNombres.Tag == null || string.IsNullOrEmpty(txtNombres.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente antes de agregar productos.", "Validación", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbProductos.SelectedItem is ComboBoxItem selectedItem && selectedItem.Value is Producto producto)
            {
                // Validar cantidad
                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Por favor ingrese una cantidad válida mayor a 0.", "Validación", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar stock disponible
                if (cantidad > producto.Stock)
                {
                    MessageBox.Show($"La cantidad solicitada ({cantidad}) supera el stock disponible ({producto.Stock}).", 
                                  "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar que no esté ya agregado
                foreach (DataGridViewRow row in dataGridViewALL.Rows)
                {
                    if (row.Cells["Tipo"].Value?.ToString() == "Producto" && 
                        row.Cells["Id"].Value?.ToString() == producto.IdProducto.ToString())
                    {
                        MessageBox.Show("Este producto ya ha sido agregado.", "Validación", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Calcular subtotal
                decimal subtotal = producto.Precio * cantidad;

                // Agregar el producto al DataGridView
                int rowIndex = dataGridViewALL.Rows.Add();
                DataGridViewRow newRow = dataGridViewALL.Rows[rowIndex];

                newRow.Cells["Tipo"].Value = "Producto";
                newRow.Cells["Id"].Value = producto.IdProducto;
                newRow.Cells["Descripcion"].Value = producto.Nombre;
                newRow.Cells["Cantidad"].Value = cantidad;
                newRow.Cells["PrecioUnitario"].Value = $"${producto.Precio:F2}";
                newRow.Cells["Subtotal"].Value = $"${subtotal:F2}";
                newRow.Cells["ObjectData"].Value = producto;

                // Recalcular totales
                CalculateTotals();

                // Limpiar selecciones
                cmbProductos.SelectedIndex = -1;
                txtCantidad.Text = "";
            }
            else
            {
                MessageBox.Show("Por favor seleccione un producto.", "Validación", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CalculateTotals()
        {
            decimal totalSubtotal = 0;

            foreach (DataGridViewRow row in dataGridViewALL.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                {
                    string subtotalText = row.Cells["Subtotal"].Value.ToString().Replace("$", "");
                    if (decimal.TryParse(subtotalText, out decimal rowSubtotal))
                    {
                        totalSubtotal += rowSubtotal;
                    }
                }
            }

            decimal iva = totalSubtotal * 0.15m; // 15% IVA
            decimal total = totalSubtotal + iva;

            txtSubtotal.Text = $"${totalSubtotal:F2}";
            txtIva.Text = $"${iva:F2}";
            txtTotal.Text = $"${total:F2}";
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que hay un cliente seleccionado
            if (txtNombres.Tag == null || string.IsNullOrEmpty(txtNombres.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente antes de procesar la venta.", "Validación", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que el DataGridView no esté vacío
            if (dataGridViewALL.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto o servicio para procesar la venta.", "Validación", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar la venta
            var result = MessageBox.Show($"¿Confirma el procesamiento de la venta por un total de {txtTotal.Text}?", 
                                       "Confirmar venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                await ProcessSaleTransactionally();
            }
        }

        private async Task ProcessSaleTransactionally()
        {
            int idVenta = 0;
            bool ventaCreada = false;
            
            try
            {
                // Validar método de pago
                if (cmbMetodoPago.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un método de pago.", "Validación", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Deshabilitar controles durante el proceso
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                
                // Obtener el ID del cliente
                int idCliente = Convert.ToInt32(txtNombres.Tag);
                
                // Calcular el total sin formato
                decimal total = CalculateNumericTotal();
                decimal subtotal = total / 1.12m; // Asumiendo IVA del 12%
                decimal iva = total - subtotal;
                
                System.Diagnostics.Debug.WriteLine($"Procesando venta - Cliente: {idCliente}, Total: {total}");
                
                // Crear la venta principal
                var venta = new Venta
                {
                    Fecha = DateTime.Now,
                    Total = total,
                    IdCliente = idCliente,
                    IdUsuario = _idUsuario
                };

                // Validar la venta
                if (!venta.IsValid())
                {
                    MessageBox.Show("Los datos de la venta no son válidos. Verifique que todos los campos estén completos.", "Error de Validación", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Guardar la venta principal y obtener el ID generado
                System.Diagnostics.Debug.WriteLine("Guardando venta principal...");
                await _ventaService.AddVentaAsync(venta);
                ventaCreada = true;
                System.Diagnostics.Debug.WriteLine("Venta principal guardada exitosamente");
                
                // Obtener el ID de la venta recién creada
                System.Diagnostics.Debug.WriteLine("Obteniendo ID de venta creada...");
                var ventas = await _ventaService.GetVentasAsync();
                var ventaObtenida = ventas.Where(v => v.IdCliente == idCliente && v.IdUsuario == _idUsuario)
                                         .OrderByDescending(v => v.Fecha)
                                         .FirstOrDefault();

                if (ventaObtenida == null)
                {
                    throw new Exception("No se pudo obtener el ID de la venta creada.");
                }

                idVenta = ventaObtenida.IdVenta;
                System.Diagnostics.Debug.WriteLine($"ID de venta obtenido: {idVenta}");

                // Procesar cada elemento del DataGridView
                var ventaProductosToSave = new List<VentaProducto>();
                var ventaReparacionesToSave = new List<VentaReparacion>();

                foreach (DataGridViewRow row in dataGridViewALL.Rows)
                {
                    string tipo = row.Cells["Tipo"].Value?.ToString();
                    int id = Convert.ToInt32(row.Cells["Id"].Value);
                    decimal subtotalItem = ParseCurrency(row.Cells["Subtotal"].Value?.ToString());

                    if (tipo == "Producto")
                    {
                        int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                        var ventaProducto = new VentaProducto
                        {
                            IdVenta = idVenta,
                            IdProducto = id,
                            Cantidad = cantidad,
                            Subtotal = subtotalItem
                        };

                        if (ventaProducto.IsValid())
                        {
                            ventaProductosToSave.Add(ventaProducto);
                        }
                        else
                        {
                            throw new Exception($"Datos inválidos para el producto ID: {id}");
                        }
                    }
                    else if (tipo == "Reparación")
                    {
                        var ventaReparacion = new VentaReparacion
                        {
                            IdVenta = idVenta,
                            IdReparacion = id,
                            Subtotal = subtotalItem
                        };

                        if (ventaReparacion.IsValid())
                        {
                            ventaReparacionesToSave.Add(ventaReparacion);
                        }
                        else
                        {
                            throw new Exception($"Datos inválidos para la reparación ID: {id}");
                        }
                    }
                }

                // Guardar todos los productos de la venta
                System.Diagnostics.Debug.WriteLine($"Guardando {ventaProductosToSave.Count} productos de venta...");
                foreach (var ventaProducto in ventaProductosToSave)
                {
                    await _ventaProductoService.AddVentaProductoAsync(ventaProducto);
                }

                // Guardar todas las reparaciones de la venta
                System.Diagnostics.Debug.WriteLine($"Guardando {ventaReparacionesToSave.Count} reparaciones de venta...");
                foreach (var ventaReparacion in ventaReparacionesToSave)
                {
                    await _ventaReparacionService.AddVentaReparacionAsync(ventaReparacion);
                }
                
                System.Diagnostics.Debug.WriteLine("Todos los items de venta guardados exitosamente");

                // Intentar crear la factura y pago (funcionalidad opcional)
                Factura facturaCreada = null;
                bool facturaYPagoCreados = false;
                string mensajeFacturacion = "";

                // PASO CRÍTICO: La venta ya está guardada en BD. Lo que sigue es opcional.
                try
                {
                    System.Diagnostics.Debug.WriteLine("Intentando crear factura...");
                    // Crear la factura
                    var factura = new Factura
                    {
                        Numero = $"FAC-{DateTime.Now:yyyyMMdd}-{idVenta:D6}",
                        FechaEmision = DateTime.Now,
                        Subtotal = subtotal,
                        IVA = iva,
                        Total = total,
                        Estado = "EMITIDA",
                        IdVenta = idVenta,
                        IdCliente = idCliente
                    };

                    if (!factura.IsValid())
                    {
                        throw new Exception("Los datos de la factura no son válidos.");
                    }

                    // Crear la factura y obtener la factura creada con su ID directamente
                    facturaCreada = await _facturaService.AddFacturaAsync(factura);

                    if (facturaCreada == null)
                    {
                        throw new Exception("No se pudo crear la factura.");
                    }

                    System.Diagnostics.Debug.WriteLine($"Factura creada: {facturaCreada.Numero}");

                    // Crear el registro de pago
                    var pago = new Pago
                    {
                        Fecha = DateTime.Now,
                        Monto = total,
                        Metodo = cmbMetodoPago.Text,
                        IdFactura = facturaCreada.IdFactura
                    };

                    if (!pago.IsValid())
                    {
                        throw new Exception("Los datos del pago no son válidos.");
                    }

                    // Crear el pago (no necesitamos guardar la respuesta para este caso)
                    await _pagoService.AddPagoAsync(pago);
                    facturaYPagoCreados = true;
                    mensajeFacturacion = $"Factura: {facturaCreada.Numero}";
                    System.Diagnostics.Debug.WriteLine("Factura y pago creados exitosamente");
                }
                catch (Exception facturaEx)
                {
                    // Si falla la facturación, continuar sin ella pero registrar el error
                    System.Diagnostics.Debug.WriteLine($"Error en facturación: {facturaEx.Message}");
                    mensajeFacturacion = "Advertencia: No se pudo generar la factura (API no disponible)";
                    facturaCreada = new Factura 
                    { 
                        Numero = $"FAC-{DateTime.Now:yyyyMMdd}-{idVenta:D6}",
                        FechaEmision = DateTime.Now,
                        Subtotal = subtotal,
                        IVA = iva,
                        Total = total,
                        Estado = "PENDIENTE",
                        IdVenta = idVenta,
                        IdCliente = idCliente
                    };
                }

                // Obtener información del cliente para el email
                var clientes = await _clienteService.GetClientesAsync();
                var cliente = clientes.FirstOrDefault(c => c.IdCliente == idCliente);

                if (cliente != null && !string.IsNullOrEmpty(cliente.Correo))
                {
                    try
                    {
                        var emailService = new EmailService();
                        bool emailEnviado = await emailService.EnviarComprobanteAsync(cliente, facturaCreada, ventaProductosToSave, ventaReparacionesToSave);
                        
                        if (emailEnviado)
                        {
                            MessageBox.Show($"Venta procesada exitosamente.\nID de Venta: {idVenta}\n{mensajeFacturacion}\nTotal: {txtTotal.Text}\n\nComprobante enviado al correo: {cliente.Correo}", 
                                          "Venta Completada", MessageBoxButtons.OK, facturaYPagoCreados ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Venta procesada exitosamente.\nID de Venta: {idVenta}\n{mensajeFacturacion}\nTotal: {txtTotal.Text}\n\nAdvertencia: No se pudo enviar el comprobante por correo.", 
                                          "Venta Completada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception emailEx)
                    {
                        MessageBox.Show($"Venta procesada exitosamente.\nID de Venta: {idVenta}\n{mensajeFacturacion}\nTotal: {txtTotal.Text}\n\nError al enviar correo: {emailEx.Message}", 
                                      "Venta Completada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Venta procesada exitosamente.\nID de Venta: {idVenta}\n{mensajeFacturacion}\nTotal: {txtTotal.Text}\n\nNo se pudo enviar comprobante: cliente sin correo electrónico.", 
                                  "Venta Completada", MessageBoxButtons.OK, facturaYPagoCreados ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }

                // Limpiar el formulario después del éxito
                ClearFormAfterSale();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR PRINCIPAL: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                
                string errorMessage = $"Error al procesar la venta: {ex.Message}";
                
                // Si la venta ya se guardó, informar al usuario
                if (ventaCreada && idVenta > 0)
                {
                    errorMessage += $"\n\nNOTA IMPORTANTE: La venta principal (ID: {idVenta}) SÍ se guardó en la base de datos, pero hubo problemas con procesos adicionales.";
                    MessageBox.Show(errorMessage, "Venta Parcialmente Completada", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    // Si la venta se guardó, limpiar el formulario
                    ClearFormAfterSale();
                }
                else
                {
                    errorMessage += "\n\nLos datos se han mantenido para que pueda intentar nuevamente.";
                    MessageBox.Show(errorMessage, "Error en la Transacción", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                // Rehabilitar controles
                btnGuardar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private decimal CalculateNumericTotal()
        {
            decimal totalSubtotal = 0;

            foreach (DataGridViewRow row in dataGridViewALL.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                {
                    string subtotalText = row.Cells["Subtotal"].Value.ToString();
                    decimal rowSubtotal = ParseCurrency(subtotalText);
                    
                    // Debug temporal: mostrar cada subtotal
                    System.Diagnostics.Debug.WriteLine($"Subtotal texto: '{subtotalText}' -> Valor parseado: {rowSubtotal}");
                    
                    totalSubtotal += rowSubtotal;
                }
            }

            decimal iva = totalSubtotal * 0.15m; // 15% IVA
            decimal total = totalSubtotal + iva;
            
            // Debug temporal: mostrar cálculo final
            System.Diagnostics.Debug.WriteLine($"Total subtotales: {totalSubtotal}, IVA: {iva}, Total final: {total}");
            
            return total;
        }

        private decimal ParseCurrency(string currencyText)
        {
            if (string.IsNullOrEmpty(currencyText))
                return 0;

            // Remover el símbolo de moneda y espacios
            string cleanText = currencyText.Replace("$", "").Trim();
            
            // Debug temporal: mostrar el texto limpio
            System.Diagnostics.Debug.WriteLine($"ParseCurrency - Texto original: '{currencyText}' -> Texto limpio: '{cleanText}'");
            
            // Normalizar el separador decimal: reemplazar coma por punto
            cleanText = cleanText.Replace(",", ".");
            
            // Usar NumberStyles.Number con cultura invariante
            bool success = decimal.TryParse(cleanText, System.Globalization.NumberStyles.Number, 
                                          System.Globalization.CultureInfo.InvariantCulture, out decimal result);
            
            // Debug temporal: mostrar el resultado
            System.Diagnostics.Debug.WriteLine($"ParseCurrency - Resultado: {result}, Success: {success}");
            
            return success ? result : 0;
        }

        private void ClearFormAfterSale()
        {
            // Limpiar todos los campos y controles
            txtBuscarCliente.Text = "";
            txtNombres.Text = "";
            txtNombres.Tag = null;
            cmbReparaciones.Items.Clear();
            cmbReparaciones.SelectedIndex = -1;
            cmbProductos.SelectedIndex = -1;
            txtCantidad.Text = "";
            
            // Limpiar DataGridView
            dataGridViewALL.Rows.Clear();
            
            // Resetear totales
            txtSubtotal.Text = "$0.00";
            txtIva.Text = "$0.00";
            txtTotal.Text = "$0.00";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Confirmar acción si hay datos en el formulario
            bool hasData = !string.IsNullOrEmpty(txtBuscarCliente.Text) || 
                          !string.IsNullOrEmpty(txtNombres.Text) || 
                          dataGridViewALL.Rows.Count > 0 ||
                          !string.IsNullOrEmpty(txtCantidad.Text);

            if (hasData)
            {
                var result = MessageBox.Show("¿Está seguro de que desea cancelar y limpiar todos los datos?", 
                                           "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // Limpiar completamente todos los campos y controles
            txtBuscarCliente.Text = "";
            txtNombres.Text = "";
            txtNombres.Tag = null;
            cmbReparaciones.Items.Clear();
            cmbReparaciones.SelectedIndex = -1;
            cmbProductos.SelectedIndex = -1;
            txtCantidad.Text = "";
            
            // Limpiar DataGridView
            dataGridViewALL.Rows.Clear();
            
            // Resetear totales
            txtSubtotal.Text = "$0.00";
            txtIva.Text = "$0.00";
            txtTotal.Text = "$0.00";
        }

        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
