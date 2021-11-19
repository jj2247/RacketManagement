using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RacketManagement.Data;
using RacketManagement.Models;

namespace RacketManagement.Controllers
{
    public class RacketController : Controller
    {
        private readonly RacketManagementContext _context;

        public RacketController(RacketManagementContext context)
        {
            _context = context;
        }

        // GET: Racket
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rackets.ToListAsync());
        }

        // GET: Racket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racket = await _context.Rackets
                .FirstOrDefaultAsync(m => m.RacketID == id);
            if (racket == null)
            {
                return NotFound();
            }

            return View(racket);
        }

        // GET: Racket/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Racket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RacketID,name")] Racket racket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(racket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(racket);
        }

        // GET: Racket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racket = await _context.Rackets.FindAsync(id);
            if (racket == null)
            {
                return NotFound();
            }
            return View(racket);
        }

        // POST: Racket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RacketID,name")] Racket racket)
        {
            if (id != racket.RacketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(racket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RacketExists(racket.RacketID))
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
            return View(racket);
        }

        // GET: Racket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racket = await _context.Rackets
                .FirstOrDefaultAsync(m => m.RacketID == id);
            if (racket == null)
            {
                return NotFound();
            }

            return View(racket);
        }

        // POST: Racket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var racket = await _context.Rackets.FindAsync(id);
            _context.Rackets.Remove(racket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RacketExists(int id)
        {
            return _context.Rackets.Any(e => e.RacketID == id);
        }
    }
}
