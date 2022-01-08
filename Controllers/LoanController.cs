using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RacketManagement.Data;
using RacketManagement.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RacketManagement.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly RacketManagementContext _context;
        private IHttpContextAccessor _httpContextAccessor;


        public LoanController(RacketManagementContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Loan
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var racketManagementContext = _context.Loans
                .Include(l => l.ApplicationUser)
                .Include(l => l.Racket)
                .Include(l => l.Racket.Brand)
                .Include(l => l.Racket.Model)
                .Include(l => l.Racket.GripSize);

            if(User.Identity.IsAuthenticated)
            {
                if(!User.IsInRole("Administrator"))
                {
                    racketManagementContext = _context.Loans.Where(id => id.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                        .Include(l => l.ApplicationUser)
                        .Include(l => l.Racket)
                        .Include(l => l.Racket.Brand)
                        .Include(l => l.Racket.Model)
                        .Include(l => l.Racket.GripSize);
                }
            }
            return View(await racketManagementContext.ToListAsync());
        }

        // GET: Loan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.ApplicationUser)
                .Include(l => l.Racket)
                .FirstOrDefaultAsync(m => m.LoanID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loan/Create
        public IActionResult Create()
        {
            ViewData["RacketID"] = new SelectList(_context.Rackets, "RacketID", "RacketID");

            var listOfRacketIds = _context.Loans.Select(r => r.RacketID);

            ViewData["RacketName"] = new SelectList(from s in _context.Rackets.Where(r => !listOfRacketIds.Contains(r.RacketID)) select new {
                RacketID=s.RacketID,
                RacketName=s.Brand.name + " " + s.Model.name + " " + s.GripSize.size
            }, "RacketID", "RacketName", null);
            return View();
        }

        // POST: Loan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("LoanID,RacketID")] Loan loan)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            loan.UserId = id;
            loan.ReturnDate = DateTime.Now.AddDays(12);
            _context.Add(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Loan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", loan.UserId);
            ViewData["RacketID"] = new SelectList(_context.Rackets, "RacketID", "RacketID", loan.RacketID);
            return View(loan);
        }

        // POST: Loan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("LoanID,UserId,RacketID,ReturnDate")] Loan loan)
        {
            if (id != loan.LoanID)
            {
                return NotFound();
            }
            try
            {
                _context.Update(loan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(loan.LoanID))
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

        // GET: Loan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.ApplicationUser)
                .Include(l => l.Racket)
                .FirstOrDefaultAsync(m => m.LoanID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanID == id);
        }
    }
}
