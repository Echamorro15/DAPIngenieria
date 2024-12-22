
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAPIngenieria.Models
{
    [Table("Presupuesto")]
    public class Presupuesto
    {
        [Key]
        public int IdPresupuesto { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int IdTipoServicio { get; set; }
        public TipoServicios TipoServicio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int MontoUnitario { get; set; }
        public int MontoTotal { get; set; }
        public string UsuarioCreador { get; set; }
      
    }
}
