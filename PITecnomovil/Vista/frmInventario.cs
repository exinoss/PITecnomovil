using System;
using System.Windows.Forms;

namespace PITecnomovil
{
    public partial class frmInventario : Form
    {
        public frmInventario()
        {
            InitializeComponent();
            this.TopLevel = false; 
            this.FormBorderStyle = FormBorderStyle.None;
            this.Visible = true;
        }

        private void materialCard1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
