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
    public class GripSizeController : Controller
    {
        private readonly RacketManagementContext _context;

        public GripSizeController(RacketManagementContext context)
        {
            _context = context;
        }

        // GET: GripSize
        public async Task<IActionResult> Index()
        {
            return View(await _context.GripSizes.ToListAsync());
        }

        // GET: GripSize/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gripSize = await _context.GripSizes
                .FirstOrDefaultAsync(m => m.GripSizeID == id);
            if (gripSize == null)
            {
                return NotFound();
            }

            return View(gripSize);
        }

        // GET: GripSize/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GripSize/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GripSizeID,size")] GripSize gripSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gripSize);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Racket", new { area = "" });
            }
            return View(gripSize);
        }

        // GET: GripSize/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gripSize = await _context.GripSizes.FindAsync(id);
            if (gripSize == null)
            {
                return NotFound();
            }
            return View(gripSize);
        }

        // POST: GripSize/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GripSizeID,size")] GripSize gripSize)
        {
            if (id != gripSize.GripSizeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gripSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GripSizeExists(gripSize.GripSizeID))
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
            return View(gripSize);
        }

        // GET: GripSize/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gripSize = await _context.GripSizes
                .FirstOrDefaultAsync(m => m.GripSizeID == id);
            if (gripSize == null)
            {
                return NotFound();
            }

            return View(gripSize);
        }

        // POST: GripSize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gripSize = await _context.GripSizes.FindAsync(id);
            _context.GripSizes.Remove(gripSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GripSizeExists(int id)
        {
            return _context.GripSizes.Any(e => e.GripSizeID == id);
        }
    }
}
