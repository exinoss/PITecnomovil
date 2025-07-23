using PITecnomovil.Servicios;
using PITecnomovil.Modelo;
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
    public partial class frmProductos : Form
    {
        private int? _selectedProductId;
        private bool _RegistrarActualizar;
        private readonly IProductoService _productoService;
        private CancellationTokenSource _searchCancellationTokenSource;
        public frmProductos()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            LoadProductos();
            _RegistrarActualizar = true;
        }
        private async void LoadProductos()
        {
            try
            {
                var productos = await _productoService.GetProductosAsync();
                dataGridViewProductos.DataSource = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Clean()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            _selectedProductId = null;
            _RegistrarActualizar = true;
            btnGuardar.Text = "Guardar";
            LoadProductos();
        }
        private void frmProductos_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurarse de que se haga clic en una fila válida
            {
                var selectedRow = dataGridViewProductos.Rows[e.RowIndex];
                _selectedProductId = (int)selectedRow.Cells["IdProducto"].Value;
                txtNombre.Text = selectedRow.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = selectedRow.Cells["Descripcion"].Value?.ToString() ?? "";
                txtPrecio.Text = selectedRow.Cells["Precio"].Value.ToString();
                txtStock.Text = selectedRow.Cells["Stock"].Value.ToString();
                _RegistrarActualizar = false;
                btnGuardar.Text = "Actualizar";
            }
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _searchCancellationTokenSource?.Cancel();
            _searchCancellationTokenSource = new CancellationTokenSource();

            try
            {
                if (txtBuscar.Text != string.Empty)
                {
                    await Task.Delay(300, _searchCancellationTokenSource.Token);
                    var searchText = txtBuscar.Text;
                    var productos = await _productoService.SearchProductosAsync(searchText);
                    dataGridViewProductos.DataSource = productos;
                }
                else LoadProductos();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            string nameTypeError = "";
            try
            {
                if (_RegistrarActualizar && ConfirmarAccion("¿Estás seguro de que deseas guardar este registro?"))
                {
                    var producto = new Producto
                    {
                        Nombre = txtNombre.Text,
                        Descripcion = txtDescripcion.Text,
                        Precio = decimal.Parse(txtPrecio.Text),
                        Stock = int.Parse(txtStock.Text)
                    };
                    if (!producto.IsValid())
                    {
                        MessageBox.Show("El nombre, descripción no pueden estar vacíos, y el precio debe ser mayor a 0 y el stock no negativo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    await _productoService.AddProductoAsync(producto);
                    LoadProductos();
                    Clean();
                }
                else
                {
                    if (_selectedProductId.HasValue && ConfirmarAccion("¿Estás seguro de que deseas actualizar este registro?"))
                    {
                        var producto = new Producto
                        {
                            IdProducto = _selectedProductId.Value,
                            Nombre = txtNombre.Text,
                            Descripcion = txtDescripcion.Text,
                            Precio = decimal.Parse(txtPrecio.Text),
                            Stock = int.Parse(txtStock.Text)
                        };
                        if (!producto.IsValid())
                        {
                            MessageBox.Show("El nombre, descripción no pueden estar vacíos, y el precio debe ser mayor a 0 y el stock no negativo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        await _productoService.UpdateProductoAsync(_selectedProductId.Value, producto);
                        LoadProductos();
                        Clean();
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un producto para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                nameTypeError = _RegistrarActualizar ? "Registrar" : "Actualizar";
                MessageBox.Show($"Error al {nameTypeError}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ConfirmarAccion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                   == DialogResult.Yes;
        }
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedProductId.HasValue && ConfirmarAccion("¿Estás seguro de eliminar el registro?"))
                {
                    await _productoService.DeleteProductoAsync(_selectedProductId.Value);
                    LoadProductos();
                    Clean();
                }
                else
                {
                    MessageBox.Show("Selecciona un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void dataGridViewProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
