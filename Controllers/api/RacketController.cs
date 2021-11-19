using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacketManagement.Data;
using RacketManagement.Models;

namespace RacketManagement.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacketController : ControllerBase
    {
        private readonly RacketManagementContext _context;

        public RacketController(RacketManagementContext context)
        {
            _context = context;
        }

        // GET: api/Racket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Racket>>> GetRackets()
        {
            return await _context.Rackets.ToListAsync();
        }

        // GET: api/Racket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Racket>> GetRacket(int id)
        {
            var racket = await _context.Rackets.FindAsync(id);

            if (racket == null)
            {
                return NotFound();
            }

            return racket;
        }

        // PUT: api/Racket/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRacket(int id, Racket racket)
        {
            if (id != racket.RacketID)
            {
                return BadRequest();
            }

            _context.Entry(racket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RacketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Racket
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Racket>> PostRacket(Racket racket)
        {
            _context.Rackets.Add(racket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRacket", new { id = racket.RacketID }, racket);
        }

        // DELETE: api/Racket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRacket(int id)
        {
            var racket = await _context.Rackets.FindAsync(id);
            if (racket == null)
            {
                return NotFound();
            }

            _context.Rackets.Remove(racket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RacketExists(int id)
        {
            return _context.Rackets.Any(e => e.RacketID == id);
        }
    }
}
