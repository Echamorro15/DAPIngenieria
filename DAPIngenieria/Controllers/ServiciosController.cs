using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAPIngenieria.Data;
using DAPIngenieria.Models;

namespace DAPIngenieria.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly AppDbContext _context;

        public ServiciosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Servicios

        public async Task<IActionResult> Index()
        {

            //return View(await _context.Servicios.ToListAsync());

            var servicios = await _context.Servicio
                .Include(s => s.TipoServicio) // Incluye la relación con TipoServicio
                .Include(s => s.OrdenTrabajo) // Incluye la relación con OrdenTrabajo
                .ToListAsync();

            return View(servicios);

        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var servicio = await _context.Servicios
                .FirstOrDefaultAsync(m => m.IdServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
            
        }


        // GET: Servicios/Create
        public IActionResult Create()
        {

            var servicio = new Servicio();
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "RazonSocial", servicio.IdCliente);
            ViewData["IdOrden"] = new SelectList(_context.OrdenTrabajo, "IdOrden", "DesOrden", servicio.IdOrden);
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicios, "IdTipoServicio", "DesTipoServicio", servicio.IdTipoServicio);
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServicio,IdOrden,IdTipoServicio,IdCliente,DesServicio,FechaFin,FechaProx,UsuarioCreador")] Servicio servicio)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("OrdenTrabajo");
            ModelState.Remove("TipoServicio");
            if (ModelState.IsValid)
            {
                // Obtén el IdCliente desde la tabla OrdenTrabajo
                var orden = await _context.OrdenTrabajo
                    .Where(o => o.IdOrden == servicio.IdOrden) // Ajusta según el campo que estás utilizando
                    .Select(o => o.IdCliente) // Selecciona el IdCliente asociado
                    .FirstOrDefaultAsync();

                if (orden == null)
                {
                    ModelState.AddModelError("IdOrden", "No se encontró la orden especificada.");
                    return View(servicio);
                }

                // Asigna el IdCliente al servicio
                servicio.IdCliente = orden;
                //servicio.IdCliente = 1;
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "RazonSocial", servicio.IdCliente);
            ViewData["IdOrden"] = new SelectList(_context.OrdenTrabajo, "IdOrden", "DesOrden", servicio.IdOrden);
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicios, "IdTipoServicio", "DesTipoServicio", servicio.IdTipoServicio);
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "RazonSocial", servicio.IdCliente);
            ViewData["IdOrden"] = new SelectList(_context.OrdenTrabajo, "IdOrden", "DesOrden", servicio.IdOrden);
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicios, "IdTipoServicio", "DesTipoServicio", servicio.IdTipoServicio);
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServicio,IdOrden,IdTipoServicio,IdCliente,DesServicio,FechaFin,FechaProx,UsuarioCreador")] Servicio servicio)
        {
            if (id != servicio.IdServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.IdServicio))
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
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "RazonSocial", servicio.IdCliente);
            ViewData["IdOrden"] = new SelectList(_context.OrdenTrabajo, "IdOrden", "DesOrden", servicio.IdOrden);
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicios, "IdTipoServicio", "DesTipoServicio", servicio.IdTipoServicio);
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Cliente)
                .Include(s => s.OrdenTrabajo)
                .Include(s => s.TipoServicio)
                .FirstOrDefaultAsync(m => m.IdServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicio.Remove(servicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.IdServicio == id);
        }
    }
}
