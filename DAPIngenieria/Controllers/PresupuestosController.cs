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
    public class PresupuestosController : Controller
    {
        private readonly AppDbContext _context;

        public PresupuestosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Presupuestos


             public async Task<IActionResult> Index()
        {
            return View(await _context.Presupuestos.ToListAsync());
        }

        // GET: Presupuestos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos
                .FirstOrDefaultAsync(m => m.IdPresupuesto == id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            return View(presupuesto);
        }

        // GET: Presupuestos/Create
        public IActionResult Create()
        {  
            // Inicializar un nuevo objeto de Presupuesto para la vista
            var presupuesto = new Presupuesto();
            // Cargar lista de clientes y tipos de servicios desde la base de datos
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "RazonSocial");
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicios, "IdTipoServicio", "DesTipoServicio");
            ViewBag.Estados = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                        new SelectListItem { Value = "Aceptado", Text = "Aceptado" },
                        new SelectListItem { Value = "Rechazado", Text = "Rechazado" }
                    };
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPresupuesto,IdCliente,FechaCreacion,FechaVencimiento,IdTipoServicio,Estado,Descripcion,Cantidad,MontoUnitario,MontoTotal,UsuarioCreador")] Presupuesto presupuesto)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("TipoServicio");
            if (ModelState.IsValid)
            { 
                // Calcular MontoTotal
                presupuesto.MontoTotal = presupuesto.Cantidad * presupuesto.MontoUnitario;

                _context.Add(presupuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(presupuesto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Estados = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                        new SelectListItem { Value = "Aceptado", Text = "Aceptado" },
                        new SelectListItem { Value = "Rechazado", Text = "Rechazado" }
                    };
            if (id == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            return View(presupuesto);
        }

        // POST: Presupuestos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPresupuesto,IdCliente,FechaCreacion,FechaVencimiento,IdTipoServicio,Estado,Descripcion,Cantidad,MontoUnitario,MontoTotal,UsuarioCreador")] Presupuesto presupuesto)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("TipoServicio");
            if (id != presupuesto.IdPresupuesto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presupuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresupuestoExists(presupuesto.IdPresupuesto))
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
            return View(presupuesto);
        }

        // GET: Presupuestos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos
                .FirstOrDefaultAsync(m => m.IdPresupuesto == id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            return View(presupuesto);
        }

        // POST: Presupuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("TipoServicio");
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto != null)
            {
                _context.Presupuestos.Remove(presupuesto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresupuestoExists(int id)
        {
            return _context.Presupuestos.Any(e => e.IdPresupuesto == id);
        }
    }
}
