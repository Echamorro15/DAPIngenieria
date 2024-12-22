using System;
using System.Collections.Generic;
using DAPIngenieria.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAPIngenieria.ViewModels
{
    public class ServicioFiltroViewModel
    {
        public int? ClienteId { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int? TipoServicioId { get; set; }

        // Listas para dropdowns
        public List<SelectListItem> Clientes { get; set; }
        public List<SelectListItem> TiposServicios { get; set; }

        // Resultados filtrados
        public List<Servicio> Resultados { get; set; }
    }
}



