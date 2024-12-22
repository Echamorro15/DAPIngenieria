using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAPIngenieria.Models
{
    public class PresupuestoFiltroViewModel
    {
        public int? ClienteId { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Estado { get; set; }

        // Listas para dropdowns
        public List<SelectListItem> Clientes { get; set; }
        public List<SelectListItem> Estados { get; set; }

        // Resultados filtrados
        public List<Presupuesto> Resultados { get; set; }
    }
}
