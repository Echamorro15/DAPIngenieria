using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAPIngenieria.Models
{
    [Table("OrdenTrabajo")]
    public class OrdenTrabajo
    {
        [Key]
        [Required(ErrorMessage = "El campo IdOrden es obligatorio")]
        public int IdOrden { get; set; } 

        [Required(ErrorMessage = "La Fecha de Creación es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "La Fecha de Inicio es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El IdCliente es obligatorio")]
        public int IdCliente { get; set; }
        //public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El IdPresupuesto es obligatorio")]
        public int IdPresupuesto { get; set; }

        [Required(ErrorMessage = "El IdTipoServicio es obligatorio")]
        public int IdTipoServicio { get; set; }

        [Required(ErrorMessage = "La Descripción de la Orden es obligatoria")]
        [StringLength(255)]
        public string DesOrden { get; set; }

        [Required(ErrorMessage = "El IdEmpleado es obligatorio")]
        public int IdEmpleado { get; set; }

        public ICollection<Servicio> Servicios { get; set; }
    }
}

