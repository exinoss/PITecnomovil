using System;
using System.Configuration;

namespace PITecnomovil.Config
{
    public static class EmailConfig
    {
        public static string SmtpServer => "smtp.gmail.com";
        public static int SmtpPort => 587;
        
        // Estas credenciales deben ser configuradas en app.config
        public static string EmailFrom => ConfigurationManager.AppSettings["EmailFrom"] ?? "tu-email@gmail.com";
        public static string EmailPassword => ConfigurationManager.AppSettings["EmailPassword"] ?? "tu-app-password";
        public static string CompanyName => ConfigurationManager.AppSettings["CompanyName"] ?? "Tecnomóvil";
    }
}