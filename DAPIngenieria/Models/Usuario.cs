using System.ComponentModel.DataAnnotations;

namespace DAPIngenieria.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public string Rol { get; set; }  // "Gerencial" o "Operativo"
    }
}
