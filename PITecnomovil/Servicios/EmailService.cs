using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using PITecnomovil.Modelo;
using PITecnomovil.Config;
using System.Collections.Generic;
using System.Text;

namespace PITecnomovil.Servicios
{
    public class EmailService
    {
        private readonly string _smtpServer = EmailConfig.SmtpServer;
        private readonly int _smtpPort = EmailConfig.SmtpPort;
        private readonly string _emailFrom = EmailConfig.EmailFrom;
        private readonly string _emailPassword = EmailConfig.EmailPassword;

        public async Task<bool> EnviarComprobanteAsync(Cliente cliente, Factura factura, List<VentaProducto> productos, List<VentaReparacion> reparaciones)
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailFrom, _emailPassword);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailFrom, EmailConfig.CompanyName),
                        Subject = $"Comprobante de Venta - Factura #{factura.Numero}",
                        Body = GenerarHtmlComprobante(cliente, factura, productos, reparaciones),
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(cliente.Correo);

                    await client.SendMailAsync(mailMessage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log del error (puedes implementar logging aquí)
                Console.WriteLine($"Error al enviar email: {ex.Message}");
                return false;
            }
        }

        private string GenerarHtmlComprobante(Cliente cliente, Factura factura, List<VentaProducto> productos, List<VentaReparacion> reparaciones)
        {
            var html = new StringBuilder();
            
            html.Append(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Comprobante de Venta</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; }
        .header { text-align: center; background-color: #2c3e50; color: white; padding: 20px; }
        .info-section { margin: 20px 0; }
        .table { width: 100%; border-collapse: collapse; margin: 20px 0; }
        .table th, .table td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        .table th { background-color: #f2f2f2; }
        .total-section { text-align: right; margin: 20px 0; font-weight: bold; }
        .footer { text-align: center; margin-top: 30px; color: #666; }
    </style>
</head>
<body>
    <div class='header'>
        <h1>TECNOMÓVIL</h1>
        <h2>Comprobante de Venta</h2>
    </div>
    
    <div class='info-section'>
        <h3>Información de la Factura</h3>
        <p><strong>Número de Factura:</strong> " + factura.Numero + @"</p>
        <p><strong>Fecha de Emisión:</strong> " + factura.FechaEmision.ToString("dd/MM/yyyy HH:mm") + @"</p>
        <p><strong>Estado:</strong> " + factura.Estado + @"</p>
    </div>
    
    <div class='info-section'>
        <h3>Información del Cliente</h3>
        <p><strong>Nombre:</strong> " + cliente.Nombres + @"</p>
        <p><strong>Cédula:</strong> " + cliente.Cedula + @"</p>
        <p><strong>Contacto:</strong> " + cliente.Contacto + @"</p>
        <p><strong>Correo:</strong> " + cliente.Correo + @"</p>
    </div>");

            // Agregar productos si existen
            if (productos != null && productos.Count > 0)
            {
                html.Append(@"
    <div class='info-section'>
        <h3>Productos</h3>
        <table class='table'>
            <thead>
                <tr>
                    <th>Producto</th>
                    <th>Cantidad</th>
                    <th>Precio Unitario</th>
                    <th>Subtotal</th>
                </tr>
            </thead>
            <tbody>");

                foreach (var producto in productos)
                {
                    decimal precioUnitario = producto.Cantidad > 0 ? producto.Subtotal / producto.Cantidad : 0;
                    html.Append($@"
                <tr>
                    <td>Producto ID: {producto.IdProducto}</td>
                    <td>{producto.Cantidad}</td>
                    <td>${precioUnitario:F2}</td>
                    <td>${producto.Subtotal:F2}</td>
                </tr>");
                }

                html.Append(@"
            </tbody>
        </table>
    </div>");
            }

            // Agregar reparaciones si existen
            if (reparaciones != null && reparaciones.Count > 0)
            {
                html.Append(@"
    <div class='info-section'>
        <h3>Reparaciones</h3>
        <table class='table'>
            <thead>
                <tr>
                    <th>Reparación</th>
                    <th>Subtotal</th>
                </tr>
            </thead>
            <tbody>");

                foreach (var reparacion in reparaciones)
                {
                    html.Append($@"
                <tr>
                    <td>Reparación ID: {reparacion.IdReparacion}</td>
                    <td>${reparacion.Subtotal:F2}</td>
                </tr>");
                }

                html.Append(@"
            </tbody>
        </table>
    </div>");
            }

            html.Append($@"
    <div class='total-section'>
        <p><strong>Subtotal: ${factura.Subtotal:F2}</strong></p>
        <p><strong>IVA: ${factura.IVA:F2}</strong></p>
        <p><strong>Total: ${factura.Total:F2}</strong></p>
    </div>
    
    <div class='footer'>
        <p>Gracias por su compra</p>
        <p>Tecnomóvil - Servicio Técnico Especializado</p>
    </div>
</body>
</html>");

            return html.ToString();
        }
    }
}