namespace PITecnomovil.Vista
{
    partial class frmPasarelaPago
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webw = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webw
            // 
            this.webw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webw.Location = new System.Drawing.Point(0, 0);
            this.webw.MinimumSize = new System.Drawing.Size(20, 20);
            this.webw.Name = "webw";
            this.webw.Size = new System.Drawing.Size(800, 450);
            this.webw.TabIndex = 0;
            // 
            // frmPasarelaPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPasarelaPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPasarelaPago";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webw;
    }
}