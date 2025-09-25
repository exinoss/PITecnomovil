namespace PITecnomovil
{
    partial class frmVentas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewALL = new System.Windows.Forms.DataGridView();
            this.btnEliminar = new MaterialSkin.Controls.MaterialButton();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();
            this.txtNombres = new MaterialSkin.Controls.MaterialTextBox();
            this.txtBuscarCliente = new MaterialSkin.Controls.MaterialTextBox();
            this.cmbReparaciones = new MaterialSkin.Controls.MaterialComboBox();
            this.btnAddReparaciones = new MaterialSkin.Controls.MaterialButton();
            this.cmbProductos = new MaterialSkin.Controls.MaterialComboBox();
            this.btnAddProductos = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.txtSubtotal = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.txtIva = new MaterialSkin.Controls.MaterialLabel();
            this.btnLimpiarCliente = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.txtTotal = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewALL)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewALL
            // 
            this.dataGridViewALL.AllowUserToAddRows = false;
            this.dataGridViewALL.AllowUserToDeleteRows = false;
            this.dataGridViewALL.AllowUserToResizeColumns = false;
            this.dataGridViewALL.AllowUserToResizeRows = false;
            this.dataGridViewALL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewALL.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.dataGridViewALL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewALL.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewALL.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewALL.ColumnHeadersHeight = 30;
            this.dataGridViewALL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewALL.EnableHeadersVisualStyles = false;
            this.dataGridViewALL.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridViewALL.Location = new System.Drawing.Point(0, 190);
            this.dataGridViewALL.Name = "dataGridViewALL";
            this.dataGridViewALL.ReadOnly = true;
            this.dataGridViewALL.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewALL.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewALL.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewALL.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewALL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewALL.Size = new System.Drawing.Size(918, 267);
            this.dataGridViewALL.TabIndex = 26;
            // 
            // btnEliminar
            // 
            this.btnEliminar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEliminar.BackColor = System.Drawing.Color.Black;
            this.btnEliminar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnEliminar.Depth = 0;
            this.btnEliminar.HighEmphasis = true;
            this.btnEliminar.Icon = null;
            this.btnEliminar.Location = new System.Drawing.Point(809, 12);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnEliminar.Size = new System.Drawing.Size(96, 36);
            this.btnEliminar.TabIndex = 35;
            this.btnEliminar.Text = "Cancelar";
            this.btnEliminar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnEliminar.UseAccentColor = true;
            this.btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGuardar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnGuardar.Depth = 0;
            this.btnGuardar.HighEmphasis = true;
            this.btnGuardar.Icon = null;
            this.btnGuardar.Location = new System.Drawing.Point(817, 145);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGuardar.Size = new System.Drawing.Size(88, 36);
            this.btnGuardar.TabIndex = 34;
            this.btnGuardar.Text = "Pagar";
            this.btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGuardar.UseAccentColor = false;
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // txtNombres
            // 
            this.txtNombres.AnimateReadOnly = false;
            this.txtNombres.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombres.Depth = 0;
            this.txtNombres.Enabled = false;
            this.txtNombres.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNombres.LeadingIcon = null;
            this.txtNombres.Location = new System.Drawing.Point(297, 12);
            this.txtNombres.MaxLength = 50;
            this.txtNombres.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNombres.Multiline = false;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(375, 50);
            this.txtNombres.TabIndex = 53;
            this.txtNombres.Text = "";
            this.txtNombres.TrailingIcon = null;
            // 
            // txtBuscarCliente
            // 
            this.txtBuscarCliente.AnimateReadOnly = false;
            this.txtBuscarCliente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscarCliente.Depth = 0;
            this.txtBuscarCliente.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBuscarCliente.Hint = "Buscar cliente";
            this.txtBuscarCliente.LeadingIcon = null;
            this.txtBuscarCliente.Location = new System.Drawing.Point(8, 12);
            this.txtBuscarCliente.MaxLength = 50;
            this.txtBuscarCliente.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBuscarCliente.Multiline = false;
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.Size = new System.Drawing.Size(283, 50);
            this.txtBuscarCliente.TabIndex = 52;
            this.txtBuscarCliente.Text = "";
            this.txtBuscarCliente.TrailingIcon = null;
            // 
            // cmbReparaciones
            // 
            this.cmbReparaciones.AutoResize = false;
            this.cmbReparaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbReparaciones.Depth = 0;
            this.cmbReparaciones.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbReparaciones.DropDownHeight = 174;
            this.cmbReparaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReparaciones.DropDownWidth = 121;
            this.cmbReparaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbReparaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbReparaciones.FormattingEnabled = true;
            this.cmbReparaciones.Hint = "Reparaciones";
            this.cmbReparaciones.IntegralHeight = false;
            this.cmbReparaciones.ItemHeight = 43;
            this.cmbReparaciones.Location = new System.Drawing.Point(8, 68);
            this.cmbReparaciones.MaxDropDownItems = 4;
            this.cmbReparaciones.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbReparaciones.Name = "cmbReparaciones";
            this.cmbReparaciones.Size = new System.Drawing.Size(283, 49);
            this.cmbReparaciones.StartIndex = 0;
            this.cmbReparaciones.TabIndex = 55;
            // 
            // btnAddReparaciones
            // 
            this.btnAddReparaciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddReparaciones.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnAddReparaciones.Depth = 0;
            this.btnAddReparaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddReparaciones.HighEmphasis = true;
            this.btnAddReparaciones.Icon = null;
            this.btnAddReparaciones.Location = new System.Drawing.Point(298, 81);
            this.btnAddReparaciones.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAddReparaciones.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAddReparaciones.Name = "btnAddReparaciones";
            this.btnAddReparaciones.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnAddReparaciones.Size = new System.Drawing.Size(88, 36);
            this.btnAddReparaciones.TabIndex = 56;
            this.btnAddReparaciones.Text = "Agregar";
            this.btnAddReparaciones.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAddReparaciones.UseAccentColor = false;
            this.btnAddReparaciones.UseVisualStyleBackColor = true;
            // 
            // cmbProductos
            // 
            this.cmbProductos.AutoResize = false;
            this.cmbProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbProductos.Depth = 0;
            this.cmbProductos.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbProductos.DropDownHeight = 174;
            this.cmbProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductos.DropDownWidth = 121;
            this.cmbProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbProductos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbProductos.FormattingEnabled = true;
            this.cmbProductos.Hint = "Productos";
            this.cmbProductos.IntegralHeight = false;
            this.cmbProductos.ItemHeight = 43;
            this.cmbProductos.Location = new System.Drawing.Point(8, 123);
            this.cmbProductos.MaxDropDownItems = 4;
            this.cmbProductos.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.Size = new System.Drawing.Size(283, 49);
            this.cmbProductos.StartIndex = 0;
            this.cmbProductos.TabIndex = 57;
            // 
            // btnAddProductos
            // 
            this.btnAddProductos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddProductos.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnAddProductos.Depth = 0;
            this.btnAddProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProductos.HighEmphasis = true;
            this.btnAddProductos.Icon = null;
            this.btnAddProductos.Location = new System.Drawing.Point(298, 136);
            this.btnAddProductos.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAddProductos.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAddProductos.Name = "btnAddProductos";
            this.btnAddProductos.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnAddProductos.Size = new System.Drawing.Size(88, 36);
            this.btnAddProductos.TabIndex = 58;
            this.btnAddProductos.Text = "Agregar";
            this.btnAddProductos.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAddProductos.UseAccentColor = false;
            this.btnAddProductos.UseVisualStyleBackColor = true;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            this.materialLabel1.Location = new System.Drawing.Point(413, 81);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(81, 24);
            this.materialLabel1.TabIndex = 59;
            this.materialLabel1.Text = "Subtotal:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.AutoSize = true;
            this.txtSubtotal.Depth = 0;
            this.txtSubtotal.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSubtotal.Location = new System.Drawing.Point(509, 81);
            this.txtSubtotal.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.Size = new System.Drawing.Size(41, 19);
            this.txtSubtotal.TabIndex = 60;
            this.txtSubtotal.Text = "$0.00";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            this.materialLabel3.Location = new System.Drawing.Point(413, 105);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(38, 24);
            this.materialLabel3.TabIndex = 61;
            this.materialLabel3.Text = "IVA:";
            this.materialLabel3.Click += new System.EventHandler(this.materialLabel3_Click);
            // 
            // txtIva
            // 
            this.txtIva.AutoSize = true;
            this.txtIva.Depth = 0;
            this.txtIva.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtIva.Location = new System.Drawing.Point(509, 105);
            this.txtIva.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtIva.Name = "txtIva";
            this.txtIva.Size = new System.Drawing.Size(41, 19);
            this.txtIva.TabIndex = 62;
            this.txtIva.Text = "$0.00";
            // 
            // btnLimpiarCliente
            // 
            this.btnLimpiarCliente.AutoSize = false;
            this.btnLimpiarCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLimpiarCliente.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLimpiarCliente.Depth = 0;
            this.btnLimpiarCliente.HighEmphasis = true;
            this.btnLimpiarCliente.Icon = global::PITecnomovil.Properties.Resources.Limpiar;
            this.btnLimpiarCliente.Location = new System.Drawing.Point(677, 17);
            this.btnLimpiarCliente.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarCliente.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarCliente.Name = "btnLimpiarCliente";
            this.btnLimpiarCliente.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarCliente.Size = new System.Drawing.Size(40, 36);
            this.btnLimpiarCliente.TabIndex = 54;
            this.btnLimpiarCliente.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarCliente.UseAccentColor = false;
            this.btnLimpiarCliente.UseVisualStyleBackColor = true;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            this.materialLabel5.Location = new System.Drawing.Point(413, 145);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(52, 24);
            this.materialLabel5.TabIndex = 63;
            this.materialLabel5.Text = "Total:";
            // 
            // txtTotal
            // 
            this.txtTotal.AutoSize = true;
            this.txtTotal.Depth = 0;
            this.txtTotal.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTotal.Location = new System.Drawing.Point(509, 150);
            this.txtTotal.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(41, 19);
            this.txtTotal.TabIndex = 64;
            this.txtTotal.Text = "$0.00";
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 457);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.txtIva);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.btnAddProductos);
            this.Controls.Add(this.cmbProductos);
            this.Controls.Add(this.btnAddReparaciones);
            this.Controls.Add(this.cmbReparaciones);
            this.Controls.Add(this.btnLimpiarCliente);
            this.Controls.Add(this.txtNombres);
            this.Controls.Add(this.txtBuscarCliente);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dataGridViewALL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVentas";
            this.Text = "frmVentas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewALL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dataGridViewALL;
        private MaterialSkin.Controls.MaterialButton btnEliminar;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
        private MaterialSkin.Controls.MaterialButton btnLimpiarCliente;
        private MaterialSkin.Controls.MaterialTextBox txtNombres;
        private MaterialSkin.Controls.MaterialTextBox txtBuscarCliente;
        private MaterialSkin.Controls.MaterialComboBox cmbReparaciones;
        private MaterialSkin.Controls.MaterialButton btnAddReparaciones;
        private MaterialSkin.Controls.MaterialComboBox cmbProductos;
        private MaterialSkin.Controls.MaterialButton btnAddProductos;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel txtSubtotal;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel txtIva;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel txtTotal;
    }
}
