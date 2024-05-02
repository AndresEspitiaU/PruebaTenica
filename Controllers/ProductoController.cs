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
    public class ProductoController : Controller
    {
        private readonly PruebaTecnicaContext _context;

        public ProductoController(PruebaTecnicaContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos.ToListAsync();

            foreach (var Producto in productos)
            {
               if (Producto.ProCantidad == null)
                {
                   var cantidadEntrada = _context.Movimientos.Where(m => m.ProId == Producto.ProId && m.MovTipo == "Entrada").Sum(m => decimal.Parse(m.MovCantidad));

                    var costoCompra = _context.Movimientos.Where(m => m.ProId == Producto.ProId && m.MovTipo == "Entrada").Sum(m => Convert.ToDecimal(m.MovValor) * Convert.ToDecimal(m.MovCantidad));

                    Producto.ProCantidad = cantidadEntrada.ToString();
                    Producto.ProCostocompra = costoCompra;
               }
            }

            await _context.SaveChangesAsync();

            return View(productos);
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProId,ProCodigo,ProNombre,ProDescripcion,ProDetalle,ProPresentacion,ProMarca,ProProveedor,ProCostocompra,ProCostoventa1,ProCostoventa2,ProCantidad,ProCanmax,ProCanmini,ProActivo")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProId,ProCodigo,ProNombre,ProDescripcion,ProDetalle,ProPresentacion,ProMarca,ProProveedor,ProCostocompra,ProCostoventa1,ProCostoventa2,ProCantidad,ProCanmax,ProCanmini,ProActivo")] Producto producto)
        {
            if (id != producto.ProId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProId))
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
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProId == id);
        }
    }
}
