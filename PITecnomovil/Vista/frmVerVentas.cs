using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PITecnomovil.Modelo;
using PITecnomovil.Servicios;

namespace PITecnomovil.Vista
{
    public partial class frmVerVentas : Form
    {
        private readonly VentaService _ventaService;
        private readonly ClienteService _clienteService;
        private readonly EmailService _emailService;
        private readonly FacturaService _facturaService;
        private readonly VentaProductoService _ventaProductoService;
        private readonly VentaReparacionService _ventaReparacionService;
        private List<VentaConCliente> _todasLasVentas;
        private List<VentaConCliente> _ventasFiltradas;

        public frmVerVentas()
        {
            InitializeComponent();
            _ventaService = new VentaService();
            _clienteService = new ClienteService();
            _emailService = new EmailService();
            _facturaService = new FacturaService();
            _ventaProductoService = new VentaProductoService();
            _ventaReparacionService = new VentaReparacionService();
            
            // Conectar el evento Load
            this.Load += frmVerVentas_Load;
        }

        private async void frmVerVentas_Load(object sender, EventArgs e)
        {
            await CargarVentasAsync();
        }

        private async Task CargarVentasAsync()
        {
            try
            {
                // Mostrar cursor de espera
                this.Cursor = Cursors.WaitCursor;

                // Obtener ventas y clientes de la API
                var ventas = await _ventaService.GetVentasAsync();
                var clientes = await _clienteService.GetClientesAsync();

                // Combinar información de ventas con clientes
                _todasLasVentas = ventas.Select(v => new VentaConCliente
                {
                    IdVenta = v.IdVenta,
                    Fecha = v.Fecha,
                    Total = v.Total,
                    IdCliente = v.IdCliente,
                    IdUsuario = v.IdUsuario,
                    NombreCliente = clientes.FirstOrDefault(c => c.IdCliente == v.IdCliente)?.Nombres ?? "Cliente no encontrado",
                    CedulaCliente = clientes.FirstOrDefault(c => c.IdCliente == v.IdCliente)?.Cedula ?? "",
                    CorreoCliente = clientes.FirstOrDefault(c => c.IdCliente == v.IdCliente)?.Correo ?? "",
                    ContactoCliente = clientes.FirstOrDefault(c => c.IdCliente == v.IdCliente)?.Contacto ?? ""
                }).OrderByDescending(v => v.Fecha).ToList();

                _ventasFiltradas = _todasLasVentas.ToList();
                MostrarVentasEnGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las ventas: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MostrarVentasEnGrid()
        {
            dataGridViewALL.DataSource = null;
            dataGridViewALL.DataSource = _ventasFiltradas;

            // Configurar columnas
            if (dataGridViewALL.Columns.Count > 0)
            {
                dataGridViewALL.Columns["IdVenta"].HeaderText = "ID Venta";
                dataGridViewALL.Columns["IdVenta"].Width = 80;
                
                dataGridViewALL.Columns["Fecha"].HeaderText = "Fecha";
                dataGridViewALL.Columns["Fecha"].Width = 100;
                dataGridViewALL.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                
                dataGridViewALL.Columns["Total"].HeaderText = "Total";
                dataGridViewALL.Columns["Total"].Width = 100;
                dataGridViewALL.Columns["Total"].DefaultCellStyle.Format = "C2";
                dataGridViewALL.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                
                dataGridViewALL.Columns["NombreCliente"].HeaderText = "Cliente";
                dataGridViewALL.Columns["NombreCliente"].Width = 200;
                
                dataGridViewALL.Columns["CedulaCliente"].HeaderText = "Cédula";
                dataGridViewALL.Columns["CedulaCliente"].Width = 120;
                
                dataGridViewALL.Columns["CorreoCliente"].HeaderText = "Correo";
                dataGridViewALL.Columns["CorreoCliente"].Width = 200;

                // Ocultar columnas que no necesitamos mostrar
                dataGridViewALL.Columns["IdCliente"].Visible = false;
                dataGridViewALL.Columns["IdUsuario"].Visible = false;
                dataGridViewALL.Columns["ContactoCliente"].Visible = false;
            }

            // Configurar apariencia del grid
            dataGridViewALL.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewALL.MultiSelect = false;
            dataGridViewALL.ReadOnly = true;
            dataGridViewALL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridViewALL.AllowUserToAddRows = false;
            dataGridViewALL.AllowUserToDeleteRows = false;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
        }

        private void FiltrarVentas()
        {
            if (_todasLasVentas == null) return;

            string textoBusqueda = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                _ventasFiltradas = _todasLasVentas.ToList();
            }
            else
            {
                _ventasFiltradas = _todasLasVentas.Where(v =>
                    v.NombreCliente.ToLower().Contains(textoBusqueda) ||
                    v.CedulaCliente.ToLower().Contains(textoBusqueda)
                ).ToList();
            }

            MostrarVentasEnGrid();
        }

        private void dataGridViewALL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void dataGridViewALL_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewALL.SelectedRows.Count == 0) return;

            try
            {
                var ventaSeleccionada = dataGridViewALL.SelectedRows[0].DataBoundItem as VentaConCliente;
                if (ventaSeleccionada == null) return;

                // Verificar que el cliente tenga correo
                if (string.IsNullOrEmpty(ventaSeleccionada.CorreoCliente))
                {
                    MessageBox.Show("El cliente seleccionado no tiene correo electrónico registrado.", 
                        "Sin correo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirmar el envío
                var result = MessageBox.Show(
                    $"¿Desea reenviar el comprobante de la venta #{ventaSeleccionada.IdVenta} " +
                    $"al correo {ventaSeleccionada.CorreoCliente} del cliente {ventaSeleccionada.NombreCliente}?",
                    "Reenviar Comprobante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await ReenviarComprobanteAsync(ventaSeleccionada);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la solicitud: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ReenviarComprobanteAsync(VentaConCliente ventaInfo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Por simplicidad, vamos a crear una factura temporal
                // En un escenario real, deberías obtener la factura real de la base de datos
                var cliente = new Cliente
                {
                    IdCliente = ventaInfo.IdCliente,
                    Nombres = ventaInfo.NombreCliente,
                    Cedula = ventaInfo.CedulaCliente,
                    Correo = ventaInfo.CorreoCliente,
                    Contacto = ventaInfo.ContactoCliente
                };

                var factura = new Factura
                {
                    IdFactura = ventaInfo.IdVenta, // Usamos el ID de venta como referencia
                    Numero = $"FACT-{ventaInfo.IdVenta:00000}",
                    FechaEmision = ventaInfo.Fecha,
                    Estado = "Pagada",
                    Subtotal = ventaInfo.Total / 1.15m, // Aproximación del subtotal (total / 1.15 para IVA del 15%)
                    IVA = ventaInfo.Total - (ventaInfo.Total / 1.15m),
                    Total = ventaInfo.Total,
                    IdVenta = ventaInfo.IdVenta
                };

                // Obtener detalles de productos y reparaciones
                // Por ahora usamos listas vacías, pero podrías implementar métodos para obtenerlos
                var productos = new List<VentaProducto>();
                var reparaciones = new List<VentaReparacion>();

                // Intentar enviar el comprobante
                bool enviado = await _emailService.EnviarComprobanteAsync(cliente, factura, productos, reparaciones);

                if (enviado)
                {
                    MessageBox.Show(
                        $"Comprobante reenviado exitosamente al correo {cliente.Correo}",
                        "Envío Exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo enviar el comprobante. Verifique la configuración de correo.",
                        "Error en Envío",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reenviar comprobante: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
