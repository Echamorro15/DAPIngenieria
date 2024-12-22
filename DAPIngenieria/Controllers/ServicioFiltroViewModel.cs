using DAPIngenieria.Data;
using DAPIngenieria.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DAPIngenieria.Controllers;
public class ServicioFiltroViewModelController : Controller
{
    private readonly AppDbContext _context;

    public ServicioFiltroViewModelController(AppDbContext context)
    {
        _context = context;
    }

    // Acción para mostrar el formulario de filtro
    public IActionResult Index()
    {
        var model = new ServicioFiltroViewModel
        {
            Clientes = _context.Cliente
                .Select(c => new SelectListItem
                {
                    Value = c.IdCliente.ToString(),
                    Text = c.RazonSocial
                }).ToList(),
            TiposServicios = _context.TipoServicios
                .Select(ts => new SelectListItem
                {
                    Value = ts.IdTipoServicio.ToString(),
                    Text = ts.DesTipoServicio
                }).ToList()
        };

        return View(model);
    }

    // Acción para manejar el formulario y mostrar resultados
    [HttpPost]
    public async Task<IActionResult> Index(ServicioFiltroViewModel model)
    {
        var query = _context.Servicio
            .Include(s => s.Cliente)
            .Include(s => s.TipoServicio)
            .AsQueryable();

        if (model.ClienteId.HasValue)
        {
            query = query.Where(s => s.IdCliente == model.ClienteId.Value);
        }

        if (model.FechaDesde.HasValue)
        {
            query = query.Where(s => s.FechaProx >= model.FechaDesde.Value);
        }

        if (model.FechaHasta.HasValue)
        {
            query = query.Where(s => s.FechaProx <= model.FechaHasta.Value);
        }

        if (model.TipoServicioId.HasValue)
        {
            query = query.Where(s => s.IdTipoServicio == model.TipoServicioId.Value);
        }

        model.Clientes = _context.Cliente
            .Select(c => new SelectListItem
            {
                Value = c.IdCliente.ToString(),
                Text = c.RazonSocial
            }).ToList();

        model.TiposServicios = _context.TipoServicios
            .Select(ts => new SelectListItem
            {
                Value = ts.IdTipoServicio.ToString(),
                Text = ts.DesTipoServicio
            }).ToList();

        model.Resultados = await query.ToListAsync();

        return View(model);
    }
}

