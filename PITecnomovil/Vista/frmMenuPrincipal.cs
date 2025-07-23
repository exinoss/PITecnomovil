using MaterialSkin.Controls;
using PITecnomovil.Modelo;
using PITecnomovil.Servicios;
using PITecnomovil.Vista;
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

namespace PITecnomovil
{
    public partial class frmMenuPrincipal : MaterialForm
    {
        private string _rol;
        public frmMenuPrincipal(string rol)
        {
            InitializeComponent();
            _rol = rol;
        }
        
        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            textBox1.Text = _rol; 
        }
        private void frmMenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dlg = MessageBox.Show("¿Estás seguro de que deseas cerrar sesión?",
                              "Confirmación",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);

            if (dlg == DialogResult.No)
            {
                e.Cancel = true;     
                return;
            }

            e.Cancel = true;        
            this.Hide();           
            var login = new frmLogin();
            login.Show();
        }
    }
}
