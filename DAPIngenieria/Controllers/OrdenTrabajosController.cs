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
    public class OrdenTrabajosController : Controller
    {
        private readonly AppDbContext _context;

        public OrdenTrabajosController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> IndexOrden()
        {
            return View(await _context.OrdenTrabajo.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenTrabajo = await _context.OrdenTrabajo
                .FirstOrDefaultAsync(m => m.IdOrden == id);
            if (ordenTrabajo == null)
            {
                return NotFound();
            }

            return View(ordenTrabajo);
        }


        // GET: OrdenTrabajos
        public async Task<IActionResult> Index()
        {
            //Obtener solo los presupuestos con estado 'aceptado'
            var presupuestosAceptados = await (from presupuesto in _context.Presupuestos
                                               join cliente in _context.Cliente
                                               on presupuesto.IdCliente equals cliente.IdCliente
                                               where presupuesto.Estado == "aceptado"
                                               select new
                                               {
                                                   presupuesto.IdPresupuesto,
                                                   presupuesto.Descripcion,
                                                   presupuesto.Estado,
                                                   RazonSocial = cliente.RazonSocial
                                               }).ToListAsync();

            return View(presupuestosAceptados);
            //return View(await _context.OrdenTrabajo.ToListAsync());
        }

        // GET: OrdenTrabajos/Create
        public IActionResult Create(int? idPresupuesto)
        {
            if (idPresupuesto != null)
            {
                // Consulta el presupuesto seleccionado junto con los datos relacionados
                var presupuesto = _context.Presupuestos
                    .Include(p => p.Cliente) // Relación con Cliente
                    .Include(p => p.TipoServicio) // Relación con TipoServicio
                    .FirstOrDefault(p => p.IdPresupuesto == idPresupuesto);

                if (presupuesto != null)
                {
                    // Inicializar el modelo con los datos del presupuesto
                    var ordenTrabajo = new OrdenTrabajo
                    {
                        IdPresupuesto = presupuesto.IdPresupuesto,
                        IdCliente = presupuesto.IdCliente, // Relación con Cliente
                        //RazonSocial = presupuesto.Cliente.RazonSocial,
                        IdTipoServicio = presupuesto.IdTipoServicio, // Relación con Tipo de Servicio
                        DesOrden = presupuesto.Descripcion, // Descripción del presupuesto
                        FechaCreacion = DateTime.Now
                    };

                    // Cargar listas desplegables para Estado
                    ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "RazonSocial", ordenTrabajo.IdCliente);
                    ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicios, "IdTipoServicio", "DesTipoServicio", ordenTrabajo.IdTipoServicio);
                    ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombres");
                    ViewBag.Estados = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                        new SelectListItem { Value = "Aceptado", Text = "Aceptado" },
                        new SelectListItem { Value = "Rechazado", Text = "Rechazado" }
                    };

                    return View(ordenTrabajo);
                }
            }

            // Si no se encontró el presupuesto o no se pasó IdPresupuesto
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "RazonSocial");
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicios, "IdTipoServicio", "DesTipoServicio");
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombres");

            return View();
        }

        // POST: OrdenTrabajos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrdenTrabajo ordenTrabajo)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("TipoServicio");
            ModelState.Remove("Presupuesto");
            ModelState.Remove("IdEmpleado");
            ModelState.Remove("Servicios");
            ModelState.Remove("RazonSocial");
            if (ModelState.IsValid)
            {
                // Agregar y guardar la nueva Orden de Trabajo
                _context.Add(ordenTrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(ordenTrabajo);
        }



        // GET: OrdenTrabajos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var ordenTrabajo = await _context.OrdenTrabajo.FindAsync(id);
            if (ordenTrabajo == null)
            {
                return NotFound();
            }
            return View(ordenTrabajo);
        }

        // POST: OrdenTrabajos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrden,FechaCreacion,FechaInicio,IdCliente,IdPresupuesto,IdTipoServicio,DesOrden,IdEmpleado")] OrdenTrabajo ordenTrabajo)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("TipoServicio");
            ModelState.Remove("Presupuesto");
            ModelState.Remove("IdEmpleado");
            ModelState.Remove("Servicios");
            ModelState.Remove("RazonSocial");
            if (id != ordenTrabajo.IdOrden)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenTrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenTrabajoExists(ordenTrabajo.IdOrden))
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
            return View(ordenTrabajo);
        }

        // GET: OrdenTrabajos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenTrabajo = await _context.OrdenTrabajo
                .FirstOrDefaultAsync(m => m.IdOrden == id);
            if (ordenTrabajo == null)
            {
                return NotFound();
            }

            return View(ordenTrabajo);
        }

        // POST: OrdenTrabajos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenTrabajo = await _context.OrdenTrabajo.FindAsync(id);
            if (ordenTrabajo != null)
            {
                _context.OrdenTrabajo.Remove(ordenTrabajo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenTrabajoExists(int id)
        {
            return _context.OrdenTrabajo.Any(e => e.IdOrden == id);
        }
    }
}
