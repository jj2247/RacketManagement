using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RacketManagement.Data;
using RacketManagement.Models;
using Microsoft.AspNetCore.Authorization;

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
            var racketManagementContext = _context.Rackets.Include(r => r.Brand).Include(r => r.GripSize).Include(r => r.Model);
            if(User.Identity.IsAuthenticated)
            {
                if(!User.IsInRole("Administrator"))
                {
                    var listOfRacketIds = _context.Loans.Select(r => r.RacketID);
                    return View(await _context.Rackets.Include(r => r.Brand).Include(r => r.GripSize).Include(r => r.Model).Where(r => !listOfRacketIds.Contains(r.RacketID)).ToListAsync());
                }
            }
            
            return View(await racketManagementContext.ToListAsync());
        }

        // GET: Racket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racket = await _context.Rackets
                .Include(r => r.Brand)
                .Include(r => r.GripSize)
                .Include(r => r.Model)
                .FirstOrDefaultAsync(m => m.RacketID == id);
            if (racket == null)
            {
                return NotFound();
            }

            return View(racket);
        }

        // GET: Racket/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandID");
            ViewData["GripSizeID"] = new SelectList(_context.GripSizes, "GripSizeID", "GripSizeID");
            ViewData["ModelID"] = new SelectList(_context.Models, "ModelID", "ModelID");

            ViewData["BrandName"] = new SelectList(_context.Brands, "BrandID", "name");
            ViewData["ModelName"] = new SelectList(_context.Models, "ModelID", "name");
            ViewData["Gripsize"] = new SelectList(_context.GripSizes, "GripSizeID", "size");
            return View();
        }

        // POST: Racket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("RacketID,BrandID,GripSizeID,ModelID")] Racket racket)
        {
            _context.Add(racket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandID", racket.BrandID);
            ViewData["GripSizeID"] = new SelectList(_context.GripSizes, "GripSizeID", "GripSizeID", racket.GripSizeID);
            ViewData["ModelID"] = new SelectList(_context.Models, "ModelID", "ModelID", racket.ModelID);

            ViewData["BrandName"] = new SelectList(_context.Brands, "BrandID", "name");
            ViewData["ModelName"] = new SelectList(_context.Models, "ModelID", "name");
            ViewData["Gripsize"] = new SelectList(_context.GripSizes, "GripSizeID", "size");
            return View(racket);
        }

        // POST: Racket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RacketID,BrandID,GripSizeID,ModelID")] Racket racket)
        {
            if (id != racket.RacketID)
            {
                return NotFound();
            }


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
            /*
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandID", racket.BrandID);
            ViewData["GripSizeID"] = new SelectList(_context.GripSizes, "GripSizeID", "GripSizeID", racket.GripSizeID);
            ViewData["ModelID"] = new SelectList(_context.Models, "ModelID", "ModelID", racket.ModelID);
            return View(racket);*/
        }

        // GET: Racket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racket = await _context.Rackets
                .Include(r => r.Brand)
                .Include(r => r.GripSize)
                .Include(r => r.Model)
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
