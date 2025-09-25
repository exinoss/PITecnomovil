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
        private int _idUsuario;
        public frmMenuPrincipal(string rol, int idUsuario)
        {
            InitializeComponent();
            _rol = rol;
            _idUsuario = idUsuario;


            frmHome formulario0 = new frmHome();
            formulario0.Dock = DockStyle.Fill;
            materialTabControl1.TabPages[0].Controls.Add(formulario0);

            frmProductos formulario1 = new frmProductos();
            formulario1.Dock = DockStyle.Fill;
            materialTabControl1.TabPages[1].Controls.Add(formulario1);

            frmClientes formulario2 = new frmClientes();
            formulario2.Dock = DockStyle.Fill;
            materialTabControl1.TabPages[2].Controls.Add(formulario2);

            
            frmVentas formulario3 = new frmVentas(_idUsuario);
            formulario3.Dock = DockStyle.Fill;
            materialTabControl1.TabPages[3].Controls.Add(formulario3); 
            
            frmReparaciones formulario4 = new frmReparaciones(_idUsuario);
            formulario4.Dock = DockStyle.Fill;
            materialTabControl1.TabPages[4].Controls.Add(formulario4);

            if (_rol != "ADMIN")
            {
                materialTabControl1.TabPages["tabPage5"].Visible = false;
            }
            else
            {
                frmUsuarios formulario5 = new frmUsuarios();
                formulario5.Dock = DockStyle.Fill;
                materialTabControl1.TabPages[5].Controls.Add(formulario5);
            }


        }
        
        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
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
