
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAPIngenieria.Models
{
    [Table("TipoServicios")]
    public class TipoServicios
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoServicio { get; set; }

        [Required]
        public string DesTipoServicio { get; set; }

        public ICollection<Servicio> Servicios { get; set; }

    }
}
