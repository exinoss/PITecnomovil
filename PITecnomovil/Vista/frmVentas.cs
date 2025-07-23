using System;
using System.Windows.Forms;

namespace PITecnomovil
{
    public partial class frmVentas : Form
    {
        private readonly int _idUsuario;
        public frmVentas(int idUsuario)
        {
            InitializeComponent();

            _idUsuario = idUsuario;
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Visible = true;
        }
    }
}
