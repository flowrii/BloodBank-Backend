using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodAPI.Data;
using BloodAPI.Models;

namespace BloodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationCentersController : ControllerBase
    {
        private readonly BloodContext _context;

        public DonationCentersController(BloodContext context)
        {
            _context = context;
        }

        // GET: api/DonationCenters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonationCenter>>> GetDonationCenters()
        {
          if (_context.DonationCenters == null)
          {
              return NotFound();
          }
            return await _context.DonationCenters.ToListAsync();
        }

        // GET: api/DonationCenters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonationCenter>> GetDonationCenter(int id)
        {
          if (_context.DonationCenters == null)
          {
              return NotFound();
          }
            var donationCenter = await _context.DonationCenters.FindAsync(id);

            if (donationCenter == null)
            {
                return NotFound();
            }

            return donationCenter;
        }

        // PUT: api/DonationCenters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonationCenter(int id, DonationCenter donationCenter)
        {
            if (id != donationCenter.DonationCenterID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donationCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationCenterExists(id))
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

        // POST: api/DonationCenters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DonationCenter>> PostDonationCenter(DonationCenter donationCenter)
        {
          if (_context.DonationCenters == null)
          {
              return Problem("Entity set 'BloodContext.DonationCenters'  is null.");
          }
            _context.DonationCenters.Add(donationCenter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonationCenter", new { id = donationCenter.DonationCenterID }, donationCenter);
        }

        // DELETE: api/DonationCenters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonationCenter(int id)
        {
            if (_context.DonationCenters == null)
            {
                return NotFound();
            }
            var donationCenter = await _context.DonationCenters.FindAsync(id);
            if (donationCenter == null)
            {
                return NotFound();
            }

            _context.DonationCenters.Remove(donationCenter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonationCenterExists(int id)
        {
            return (_context.DonationCenters?.Any(e => e.DonationCenterID == id)).GetValueOrDefault();
        }
    }
}
