using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrionTek.Data;
using OrionTek.Models;

namespace OrionTek.Controllers
{
    public class DirectionsController : Controller
    {
        private readonly OrionTekDbContext _context;

        public DirectionsController(OrionTekDbContext context)
        {
            _context = context;
        }

        // GET: Directions
        public async Task<IActionResult> Index()
        {
            var orionTekDbContext = _context.Directions.Include(d => d.Customer);
            ViewData["CustomerName"] = new SelectList(_context.Customers, "Name", "Name");
            return View(await orionTekDbContext.ToListAsync());
        }

        // GET: Directions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Directions == null)
            {
                return NotFound();
            }

            var direction = await _context.Directions
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // GET: Directions/Create
        public IActionResult Create()
        {
            ViewData["CustomerName"] = new SelectList(_context.Customers, "Id", "Name");
            return View();
        }

        // POST: Directions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Calle,Sector,Ciudad,Provincia,Pais,CustomerId")] Direction direction)
        {

                direction.Id = Guid.NewGuid();
                _context.Add(direction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Directions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Directions == null)
            {
                return NotFound();
            }

            var direction = await _context.Directions.FindAsync(id);
            if (direction == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", direction.CustomerId);
            return View(direction);
        }

        // POST: Directions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Calle,Sector,Ciudad,Provincia,Pais,CustomerId")] Direction direction)
        {
            if (id != direction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectionExists(direction.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", direction.CustomerId);
            return View(direction);
        }

        // GET: Directions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Directions == null)
            {
                return NotFound();
            }

            var direction = await _context.Directions
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // POST: Directions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Directions == null)
            {
                return Problem("Entity set 'OrionTekDbContext.Directions'  is null.");
            }
            var direction = await _context.Directions.FindAsync(id);
            if (direction != null)
            {
                _context.Directions.Remove(direction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectionExists(Guid id)
        {
          return (_context.Directions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
