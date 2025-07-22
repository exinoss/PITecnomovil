using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_RESTful.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100, ErrorMessage = "Los nombres no pueden exceder 100 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(20, ErrorMessage = "La cédula no puede exceder 20 caracteres.")]
        [Index(IsUnique = true)]
        public string Cedula { get; set; }

        [StringLength(100, ErrorMessage = "El contacto no puede exceder 100 caracteres.")]
        public string Contacto { get; set; }
    }
}