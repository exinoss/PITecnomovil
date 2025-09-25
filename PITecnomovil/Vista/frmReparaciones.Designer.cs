namespace PITecnomovil
{
    partial class frmReparaciones
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
            this.btnNuevo = new MaterialSkin.Controls.MaterialButton();
            this.btnEliminar = new MaterialSkin.Controls.MaterialButton();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();
            this.dataGridViewALL = new System.Windows.Forms.DataGridView();
            this.txtObservaciones = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.txtNombres = new MaterialSkin.Controls.MaterialTextBox();
            this.txtBuscarCliente = new MaterialSkin.Controls.MaterialTextBox();
            this.dtFechaIngreso = new System.Windows.Forms.DateTimePicker();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.dtFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.cmbEstado = new MaterialSkin.Controls.MaterialComboBox();
            this.txtDiagnostico = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.txtPrecio = new MaterialSkin.Controls.MaterialTextBox();
            this.txtDispositivo = new MaterialSkin.Controls.MaterialTextBox();
            this.txtBuscarReparacion = new MaterialSkin.Controls.MaterialTextBox();
            this.btnLimpiarCliente = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewALL)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNuevo
            // 
            this.btnNuevo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevo.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnNuevo.Depth = 0;
            this.btnNuevo.HighEmphasis = true;
            this.btnNuevo.Icon = null;
            this.btnNuevo.Location = new System.Drawing.Point(763, 9);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnNuevo.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnNuevo.Size = new System.Drawing.Size(142, 36);
            this.btnNuevo.TabIndex = 28;
            this.btnNuevo.Text = "Nuevo registro";
            this.btnNuevo.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnNuevo.UseAccentColor = false;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEliminar.BackColor = System.Drawing.Color.Black;
            this.btnEliminar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnEliminar.Depth = 0;
            this.btnEliminar.HighEmphasis = true;
            this.btnEliminar.Icon = null;
            this.btnEliminar.Location = new System.Drawing.Point(817, 239);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnEliminar.Size = new System.Drawing.Size(88, 36);
            this.btnEliminar.TabIndex = 27;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnEliminar.UseAccentColor = true;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGuardar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnGuardar.Depth = 0;
            this.btnGuardar.HighEmphasis = true;
            this.btnGuardar.Icon = null;
            this.btnGuardar.Location = new System.Drawing.Point(721, 239);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGuardar.Size = new System.Drawing.Size(88, 36);
            this.btnGuardar.TabIndex = 26;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGuardar.UseAccentColor = false;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
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
            this.dataGridViewALL.Location = new System.Drawing.Point(0, 284);
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
            this.dataGridViewALL.Size = new System.Drawing.Size(918, 173);
            this.dataGridViewALL.TabIndex = 25;
            this.dataGridViewALL.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewALL_CellClick);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtObservaciones.Depth = 0;
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtObservaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtObservaciones.Location = new System.Drawing.Point(381, 178);
            this.txtObservaciones.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(295, 97);
            this.txtObservaciones.TabIndex = 29;
            this.txtObservaciones.Text = "";
            // 
            // txtNombres
            // 
            this.txtNombres.AnimateReadOnly = false;
            this.txtNombres.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombres.Depth = 0;
            this.txtNombres.Enabled = false;
            this.txtNombres.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNombres.LeadingIcon = null;
            this.txtNombres.Location = new System.Drawing.Point(301, 66);
            this.txtNombres.MaxLength = 50;
            this.txtNombres.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNombres.Multiline = false;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(375, 50);
            this.txtNombres.TabIndex = 40;
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
            this.txtBuscarCliente.Location = new System.Drawing.Point(12, 66);
            this.txtBuscarCliente.MaxLength = 50;
            this.txtBuscarCliente.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBuscarCliente.Multiline = false;
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.Size = new System.Drawing.Size(283, 50);
            this.txtBuscarCliente.TabIndex = 39;
            this.txtBuscarCliente.Text = "";
            this.txtBuscarCliente.TrailingIcon = null;
            this.txtBuscarCliente.TextChanged += new System.EventHandler(this.txtBuscarCliente_TextChanged);
            // 
            // dtFechaIngreso
            // 
            this.dtFechaIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFechaIngreso.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaIngreso.Location = new System.Drawing.Point(682, 148);
            this.dtFechaIngreso.Name = "dtFechaIngreso";
            this.dtFechaIngreso.Size = new System.Drawing.Size(147, 23);
            this.dtFechaIngreso.TabIndex = 41;
            this.dtFechaIngreso.ValueChanged += new System.EventHandler(this.dtFechaIngreso_ValueChanged);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(683, 123);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(104, 19);
            this.materialLabel1.TabIndex = 43;
            this.materialLabel1.Text = "Fecha Ingreso:";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(683, 182);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(106, 19);
            this.materialLabel2.TabIndex = 45;
            this.materialLabel2.Text = "Fecha Entrega:";
            // 
            // dtFechaEntrega
            // 
            this.dtFechaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaEntrega.Location = new System.Drawing.Point(682, 207);
            this.dtFechaEntrega.Name = "dtFechaEntrega";
            this.dtFechaEntrega.Size = new System.Drawing.Size(147, 23);
            this.dtFechaEntrega.TabIndex = 44;
            // 
            // cmbEstado
            // 
            this.cmbEstado.AutoResize = false;
            this.cmbEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbEstado.Depth = 0;
            this.cmbEstado.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbEstado.DropDownHeight = 174;
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.DropDownWidth = 121;
            this.cmbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Hint = "Elija el estado";
            this.cmbEstado.IntegralHeight = false;
            this.cmbEstado.ItemHeight = 43;
            this.cmbEstado.Items.AddRange(new object[] {
            "Pendiente",
            "Entregado",
            "Cancelado"});
            this.cmbEstado.Location = new System.Drawing.Point(12, 122);
            this.cmbEstado.MaxDropDownItems = 4;
            this.cmbEstado.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(155, 49);
            this.cmbEstado.StartIndex = 0;
            this.cmbEstado.TabIndex = 46;
            this.cmbEstado.SelectedIndexChanged += new System.EventHandler(this.cmbEstado_SelectedIndexChanged);
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtDiagnostico.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnostico.Depth = 0;
            this.txtDiagnostico.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDiagnostico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtDiagnostico.Location = new System.Drawing.Point(12, 178);
            this.txtDiagnostico.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.Size = new System.Drawing.Size(363, 97);
            this.txtDiagnostico.TabIndex = 47;
            this.txtDiagnostico.Text = "";
            // 
            // txtPrecio
            // 
            this.txtPrecio.AnimateReadOnly = false;
            this.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrecio.Depth = 0;
            this.txtPrecio.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPrecio.Hint = "Precio";
            this.txtPrecio.LeadingIcon = null;
            this.txtPrecio.Location = new System.Drawing.Point(564, 121);
            this.txtPrecio.MaxLength = 50;
            this.txtPrecio.MouseState = MaterialSkin.MouseState.OUT;
            this.txtPrecio.Multiline = false;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(112, 50);
            this.txtPrecio.TabIndex = 49;
            this.txtPrecio.Text = "";
            this.txtPrecio.TrailingIcon = null;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            // 
            // txtDispositivo
            // 
            this.txtDispositivo.AnimateReadOnly = false;
            this.txtDispositivo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDispositivo.Depth = 0;
            this.txtDispositivo.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDispositivo.Hint = "Ingrese nombre del dispositivo";
            this.txtDispositivo.LeadingIcon = null;
            this.txtDispositivo.Location = new System.Drawing.Point(173, 122);
            this.txtDispositivo.MaxLength = 50;
            this.txtDispositivo.MouseState = MaterialSkin.MouseState.OUT;
            this.txtDispositivo.Multiline = false;
            this.txtDispositivo.Name = "txtDispositivo";
            this.txtDispositivo.Size = new System.Drawing.Size(385, 50);
            this.txtDispositivo.TabIndex = 48;
            this.txtDispositivo.Text = "";
            this.txtDispositivo.TrailingIcon = null;
            // 
            // txtBuscarReparacion
            // 
            this.txtBuscarReparacion.AnimateReadOnly = false;
            this.txtBuscarReparacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscarReparacion.Depth = 0;
            this.txtBuscarReparacion.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBuscarReparacion.Hint = "Buscar reparación";
            this.txtBuscarReparacion.LeadingIcon = null;
            this.txtBuscarReparacion.Location = new System.Drawing.Point(12, 9);
            this.txtBuscarReparacion.MaxLength = 50;
            this.txtBuscarReparacion.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBuscarReparacion.Multiline = false;
            this.txtBuscarReparacion.Name = "txtBuscarReparacion";
            this.txtBuscarReparacion.Size = new System.Drawing.Size(664, 50);
            this.txtBuscarReparacion.TabIndex = 50;
            this.txtBuscarReparacion.Text = "";
            this.txtBuscarReparacion.TrailingIcon = null;
            this.txtBuscarReparacion.TextChanged += new System.EventHandler(this.txtBuscarReparacion_TextChanged);
            // 
            // btnLimpiarCliente
            // 
            this.btnLimpiarCliente.AutoSize = false;
            this.btnLimpiarCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLimpiarCliente.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLimpiarCliente.Depth = 0;
            this.btnLimpiarCliente.HighEmphasis = true;
            this.btnLimpiarCliente.Icon = global::PITecnomovil.Properties.Resources.Limpiar;
            this.btnLimpiarCliente.Location = new System.Drawing.Point(681, 71);
            this.btnLimpiarCliente.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarCliente.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarCliente.Name = "btnLimpiarCliente";
            this.btnLimpiarCliente.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarCliente.Size = new System.Drawing.Size(40, 36);
            this.btnLimpiarCliente.TabIndex = 51;
            this.btnLimpiarCliente.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarCliente.UseAccentColor = false;
            this.btnLimpiarCliente.UseVisualStyleBackColor = true;
            this.btnLimpiarCliente.Click += new System.EventHandler(this.btnLimpiarCliente_Click);
            // 
            // frmReparaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 457);
            this.Controls.Add(this.btnLimpiarCliente);
            this.Controls.Add(this.txtBuscarReparacion);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.txtDispositivo);
            this.Controls.Add(this.txtDiagnostico);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.dtFechaEntrega);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.dtFechaIngreso);
            this.Controls.Add(this.txtNombres);
            this.Controls.Add(this.txtBuscarCliente);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dataGridViewALL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReparaciones";
            this.Text = "frmReparaciones";
            this.Load += new System.EventHandler(this.frmReparaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewALL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private MaterialSkin.Controls.MaterialButton btnNuevo;
        private MaterialSkin.Controls.MaterialButton btnEliminar;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
        private System.Windows.Forms.DataGridView dataGridViewALL;
        private MaterialSkin.Controls.MaterialMultiLineTextBox txtObservaciones;
        private MaterialSkin.Controls.MaterialTextBox txtNombres;
        private MaterialSkin.Controls.MaterialTextBox txtBuscarCliente;
        private System.Windows.Forms.DateTimePicker dtFechaIngreso;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.DateTimePicker dtFechaEntrega;
        private MaterialSkin.Controls.MaterialComboBox cmbEstado;
        private MaterialSkin.Controls.MaterialMultiLineTextBox txtDiagnostico;
        private MaterialSkin.Controls.MaterialTextBox txtPrecio;
        private MaterialSkin.Controls.MaterialTextBox txtDispositivo;
        private MaterialSkin.Controls.MaterialTextBox txtBuscarReparacion;
        private MaterialSkin.Controls.MaterialButton btnLimpiarCliente;
    }
}
