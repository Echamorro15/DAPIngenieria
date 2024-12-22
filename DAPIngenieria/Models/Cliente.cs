using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAPIngenieria.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(20)]
        public string RUC { get; set; }

        [Required]
        [StringLength(100)]
        public string RazonSocial { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Ciudad { get; set; }

        [StringLength(50)]
        public string Departamento { get; set; }

        [StringLength(50)]
        public string Pais { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string UsuarioCreador { get; set; }

        public ICollection<Servicio> Servicios { get; set; }

    } 
        
 }

