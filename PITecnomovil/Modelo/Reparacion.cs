using System;

namespace PITecnomovil.Modelo
{
    public class Reparacion
    {
        public int IdReparacion { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Estado { get; set; }
        public string Diagnostico { get; set; }
        public string Observaciones { get; set; }
        public string Dispositivo { get; set; }
        public decimal PrecioServicio { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }

        public bool IsValid()
        {
            return IdCliente > 0 && IdUsuario > 0 && FechaIngreso != default && !string.IsNullOrEmpty(Estado) &&
                   !string.IsNullOrEmpty(Dispositivo) && PrecioServicio > 0;
        }
    }
}
