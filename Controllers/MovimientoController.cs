using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class MovimientoController : Controller
    {
        private readonly PruebaTecnicaContext _context;

        public MovimientoController(PruebaTecnicaContext context)
        {
            _context = context;
        }

        // GET: Movimiento
        public async Task<IActionResult> Index()
        {
            var pruebaTecnicaContext = _context.Movimientos.Include(m => m.Pro);
            return View(await pruebaTecnicaContext.ToListAsync());
        }

        // GET: Movimiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos
                .Include(m => m.Pro)
                .FirstOrDefaultAsync(m => m.MovId == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // GET: Movimiento/Create
        public IActionResult Create()
        {
            ViewData["ProId"] = new SelectList(_context.Productos, "ProId", "ProNombre");
            return View();
        }



        // POST: Movimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovId,MovFecha,ProId,MovTipo,MovCantidad,MovValor")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimiento);

                // Actualizar la cantidad de producto en la tabla de productos
                var producto = await _context.Productos.FindAsync(movimiento.ProId);
                if (producto != null)
                {
                    if (movimiento.MovTipo == "Entrada")
                    {

                        producto.ProCantidad = (int.Parse(producto.ProCantidad) + int.Parse(movimiento.MovCantidad)).ToString();

                    }
                    else
                    {
                        producto.ProCantidad = (int.Parse(producto.ProCantidad) - int.Parse(movimiento.MovCantidad)).ToString();
                    }
                }

                // Actualizar el costo de compra en la tabla de productos
                if (movimiento.MovTipo == "Entrada")
                {
                    producto.ProCostocompra = (producto.ProCostocompra + (decimal.Parse(movimiento.MovCantidad) * movimiento.MovValor));
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProId"] = new SelectList(_context.Productos, "ProId", "ProId", movimiento.ProId);
            return View(movimiento);
        }

        // GET: Movimiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            ViewData["ProId"] = new SelectList(_context.Productos, "ProId", "ProId", movimiento.ProId);
            return View(movimiento);
        }

        // POST: Movimiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovId,MovFecha,ProId,MovTipo,MovCantidad,MovValor")] Movimiento movimiento)
        {
            if (id != movimiento.MovId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoExists(movimiento.MovId))
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
            ViewData["ProId"] = new SelectList(_context.Productos, "ProId", "ProId", movimiento.ProId);
            return View(movimiento);
        }

        // GET: Movimiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos
                .Include(m => m.Pro)
                .FirstOrDefaultAsync(m => m.MovId == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // POST: Movimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento != null)
            {
                _context.Movimientos.Remove(movimiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.MovId == id);
        }
    }
}
