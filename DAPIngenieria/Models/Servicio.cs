using DAPIngenieria.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAPIngenieria.Models
{
    [Table("Servicio")]
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdServicio { get; set; }

        [Required]
        public int IdOrden { get; set; }

        [Required]
        public int IdTipoServicio { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(255)]
        public string DesServicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaProx { get; set; }

        [Required]
        [StringLength(100)]
        public string UsuarioCreador { get; set; }

        // Relaciones (si existen, opcional)
        [ForeignKey("IdOrden")]
        public virtual OrdenTrabajo OrdenTrabajo { get; set; }

        [ForeignKey("IdTipoServicio")]
        public virtual TipoServicios TipoServicio { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

    }
}
