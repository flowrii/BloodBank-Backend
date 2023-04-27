using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodAPI.Data;
using BloodAPI.Models;
using System.Numerics;

namespace BloodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly BloodContext _context;

        public DonorsController(BloodContext context)
        {
            _context = context;
        }

        // GET: api/Donors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donor>>> GetDonors()
        {
            if (_context.Donors == null)
            {
                return NotFound();
            }
            return await _context.Donors.ToListAsync();
        }

        // GET: api/Donors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> GetDonor(int id)
        {
            if (_context.Donors == null)
            {
                return NotFound();
            }
            var donor = await _context.Donors.FindAsync(id);

            if (donor == null)
            {
                return NotFound();
            }

            return donor;
        }

        // GET: api/Donor/username/{username}
        [HttpGet("username/{username}")]
        public async Task<ActionResult<Donor>> GetDonorByUsername(string username)
        {
            var donor = await _context.Donors.FirstOrDefaultAsync(m => m.Username == username);

            if (donor == null)
            {
                return NotFound();
            }

            return donor;
        }

        // PUT: api/Donors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonor(int id, Donor donor)
        {
            if (id != donor.DonorID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonorExists(id))
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
            return BadRequest(ModelState);
        }

        // POST: api/Donors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Donor>> CreateDonor(Donor donor)
        {
            if (ModelState.IsValid)
            {
                _context.Donors.Add(donor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDonor), new { id = donor.DonorID }, donor);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Donors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Donor>> DeleteDonor(int id)
        {
            if (_context.Donors == null)
            {
                return NotFound();
            }
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();

            return donor;
        }

        private bool DonorExists(int id)
        {
            return (_context.Donors?.Any(e => e.DonorID == id)).GetValueOrDefault();
        }
    }
}
