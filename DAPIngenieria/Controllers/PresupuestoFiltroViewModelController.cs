using DAPIngenieria.Data;
using DAPIngenieria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAPIngenieria.Controllers
{
    //public class PresupuestoFiltroViewModelController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}
    public class PresupuestoFiltroViewModelController : Controller
    {
        private readonly AppDbContext _context;

        public PresupuestoFiltroViewModelController(AppDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar el formulario de filtro
        public IActionResult Index()
        {
            var model = new PresupuestoFiltroViewModel
            {
                Clientes = _context.Cliente
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCliente.ToString(),
                        Text = c.RazonSocial
                    }).ToList(),
                Estados = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                new SelectListItem { Value = "Aceptado", Text = "Aceptado" },
                new SelectListItem { Value = "Rechazado", Text = "Rechazado" }
            }
            };

            return View(model);
        }

        // Acción para manejar el formulario y mostrar resultados
        [HttpPost]
        public IActionResult Index(PresupuestoFiltroViewModel model)
        {
            var query = _context.Presupuestos.AsQueryable();

            if (model.ClienteId.HasValue)
            {
                query = query.Where(p => p.IdCliente == model.ClienteId.Value);
            }

            if (model.FechaDesde.HasValue)
            {
                query = query.Where(p => p.FechaCreacion >= model.FechaDesde.Value);
            }

            if (model.FechaHasta.HasValue)
            {
                query = query.Where(p => p.FechaCreacion <= model.FechaHasta.Value);
            }

            if (!string.IsNullOrEmpty(model.Estado))
            {
                query = query.Where(p => p.Estado == model.Estado);
            }

            model.Clientes = _context.Cliente
                .Select(c => new SelectListItem
                {
                    Value = c.IdCliente.ToString(),
                    Text = c.RazonSocial
                }).ToList();

            model.Estados = new List<SelectListItem>
        {
            new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
            new SelectListItem { Value = "Aceptado", Text = "Aceptado" },
            new SelectListItem { Value = "Rechazado", Text = "Rechazado" }
        };

            model.Resultados = query.ToList();

            return View(model);
        }
    }

}
