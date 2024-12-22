using DAPIngenieria.Data;
using DAPIngenieria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAPIngenieria.Controllers
{
    public class TipoServiciosController : Controller
    {
        private readonly AppDbContext _context;

        public TipoServiciosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TipoServicios
        public IActionResult Index()
        {
            var tipoServicios = _context.TipoServicios.ToList();
            return View(tipoServicios);
        }

        //// GET: TipoServicios/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Create()
        {
               // Obtener el último IdPresupuesto utilizado
                int ultimoId = await _context.TipoServicios.MaxAsync(p => (int?)p.IdTipoServicio) ?? 0;

                // Incrementar en 1 para sugerir el siguiente ID
                ViewBag.SiguienteId = ultimoId + 1;

            return View();
        }


        // POST: TipoServicios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoServicio,DesTipoServicio")] TipoServicios tipoServicio)
        {
            ModelState.Remove("Servicios");
            if (ModelState.IsValid)
            {
                _context.Add(tipoServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoServicio);
        }

        // GET: TipoServicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServicio = await _context.TipoServicios.FindAsync(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }
            return View(tipoServicio);
        }

        // POST: TipoServicios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoServicio,DesTipoServicio")] TipoServicios tipoServicio)
        {
            ModelState.Remove("Servicios");
            if (id != tipoServicio.IdTipoServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoServicioExists(tipoServicio.IdTipoServicio))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoServicio);
        }

        // GET: TipoServicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServicio = await _context.TipoServicios
                .FirstOrDefaultAsync(m => m.IdTipoServicio == id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            return View(tipoServicio);
        }

        // POST: TipoServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoServicio = await _context.TipoServicios.FindAsync(id);
            if (tipoServicio != null)
            {
                _context.TipoServicios.Remove(tipoServicio);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: TipoServicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServicio = await _context.TipoServicios
                .FirstOrDefaultAsync(m => m.IdTipoServicio == id);

            if (tipoServicio == null)
            {
                return NotFound();
            }

            return View(tipoServicio);
        }


        // Método auxiliar para verificar si existe un TipoServicio
        private bool TipoServicioExists(int id)
        {
            return _context.TipoServicios.Any(e => e.IdTipoServicio == id);
        }
    }
}
