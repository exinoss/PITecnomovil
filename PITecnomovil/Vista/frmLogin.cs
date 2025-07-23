using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PITecnomovil.Properties;
using PITecnomovil.Servicios;
using PITecnomovil.Modelo;
using API_RESTful.Models;

namespace PITecnomovil.Vista
{
    public partial class frmLogin : MaterialForm
    {
        private readonly IAuthService _authService;
        public frmLogin()
        {
            InitializeComponent();
            txtClave.Password = true;

            btnOjo.AutoSize = false;                
            btnOjo.Size = new Size(36, 36);      
            btnOjo.Text = "";
            btnOjo.Icon = Resources.ojooff;




            _authService = new AuthService();
        }

        private void materialCard1_Paint(object sender, PaintEventArgs e)
        {

        }
        private bool _passwordVisible = false;

        private void btnOjo_Click(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtClave.Password = !_passwordVisible;
            btnOjo.Icon = _passwordVisible
                ? Resources.ojoon
                : Resources.ojooff;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            
            var request = new LoginRequest
            {
                NombreUsuario = txtUsuario.Text.Trim(),
                Clave = txtClave.Text.Trim()
            };
            if (!request.IsValid())
            {
                MessageBox.Show("Ingrese un usuario y una contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var result = await _authService.LoginAsync(request);

                if (result != null)
                {
                    MessageBox.Show(result.Message, "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    var menu = new frmMenuPrincipal(result.Rol,result.IdUsuario);
                    menu.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo conectar al servidor:\n{ex.Message}",
                                "Error de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
