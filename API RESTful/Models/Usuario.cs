using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_RESTful.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres.")]
        [Index(IsUnique = true)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [StringLength(100, ErrorMessage = "La clave no puede exceder 100 caracteres.")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [StringLength(20, ErrorMessage = "El rol no puede exceder 20 caracteres.")]
        public string Rol { get; set; }
    }
}