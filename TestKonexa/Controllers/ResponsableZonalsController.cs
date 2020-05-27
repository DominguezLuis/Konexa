using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestKonexa.Context;
using TestKonexa.Models;

namespace TestKonexa.Controllers
{
    public class ResponsableZonalsController : Controller
    {
        private readonly AplicationDbContext _context;

        public ResponsableZonalsController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResponsableZonals
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResponsableZonal.ToListAsync());
        }

        // GET: ResponsableZonals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsableZonal = await _context.ResponsableZonal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsableZonal == null)
            {
                return NotFound();
            }

            return View(responsableZonal);
        }

        // GET: ResponsableZonals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResponsableZonals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] ResponsableZonal responsableZonal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsableZonal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responsableZonal);
        }

        // GET: ResponsableZonals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsableZonal = await _context.ResponsableZonal.FindAsync(id);
            if (responsableZonal == null)
            {
                return NotFound();
            }
            return View(responsableZonal);
        }

        // POST: ResponsableZonals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] ResponsableZonal responsableZonal)
        {
            if (id != responsableZonal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsableZonal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsableZonalExists(responsableZonal.Id))
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
            return View(responsableZonal);
        }

        // GET: ResponsableZonals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsableZonal = await _context.ResponsableZonal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsableZonal == null)
            {
                return NotFound();
            }

            return View(responsableZonal);
        }

        // POST: ResponsableZonals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsableZonal = await _context.ResponsableZonal.FindAsync(id);
            _context.ResponsableZonal.Remove(responsableZonal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsableZonalExists(int id)
        {
            return _context.ResponsableZonal.Any(e => e.Id == id);
        }
    }
}
