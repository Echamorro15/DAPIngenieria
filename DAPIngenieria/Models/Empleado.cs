using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAPIngenieria.Models
{
    [Table("Empleado")]
    public class Empleado
    {
        [Key]
        [Required(ErrorMessage = "El IdEmpleado es obligatorio.")]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El CIN es obligatorio.")]
        [StringLength(15)]
        public string CIN { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Phone(ErrorMessage = "Número de teléfono no válido.")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Correo no válido.")]
        public string Email { get; set; }

        [StringLength(150)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        public string UsuarioCreador { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
