using System;

namespace PITecnomovil.Modelo
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Descripcion) && Precio > 0 && Stock >= 0;
        }
    }
}
