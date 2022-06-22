using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NesutapimuSistema.Data;
using NesutapimuSistema.Models;

namespace NesutapimuSistema.Controllers
{
    public class DiscrepanciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscrepanciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Discrepancies
        public async Task<IActionResult> Index()
        {
              return _context.Discrepancies != null ? 
                          View(await _context.Discrepancies.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Discrepancies'  is null.");
        }

        // GET: Discrepancies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Discrepancies == null)
            {
                return NotFound();
            }

            var discrepancies = await _context.Discrepancies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discrepancies == null)
            {
                return NotFound();
            }

            return View(discrepancies);
        }

        // GET: Discrepancies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discrepancies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Urgency,Status,Text,CreatedDate")] Discrepancies discrepancies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discrepancies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discrepancies);
        }

        // GET: Discrepancies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Discrepancies == null)
            {
                return NotFound();
            }

            var discrepancies = await _context.Discrepancies.FindAsync(id);
            if (discrepancies == null)
            {
                return NotFound();
            }
            return View(discrepancies);
        }

        // POST: Discrepancies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Urgency,Status,Text,CreatedDate")] Discrepancies discrepancies)
        {
            if (id != discrepancies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var oldDiscrepancies = await _context.Discrepancies.AsNoTracking().FirstAsync(a => a.Id==discrepancies.Id);
                discrepancies.CreatedDate = oldDiscrepancies.CreatedDate;
                discrepancies.LastUpdatedDate = DateTime.Now;
                try
                {
                    _context.Update(discrepancies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscrepanciesExists(discrepancies.Id))
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
            return View(discrepancies);
        }

        // GET: Discrepancies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Discrepancies == null)
            {
                return NotFound();
            }

            var discrepancies = await _context.Discrepancies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discrepancies == null)
            {
                return NotFound();
            }

            return View(discrepancies);
        }

        // POST: Discrepancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Discrepancies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Discrepancies'  is null.");
            }
            var discrepancies = await _context.Discrepancies.FindAsync(id);
            if (discrepancies != null)
            {
                _context.Discrepancies.Remove(discrepancies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscrepanciesExists(int id)
        {
          return (_context.Discrepancies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
