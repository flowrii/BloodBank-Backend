using BloodAPI.Data;
using BloodAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly BloodContext _context;

        public AppointmentsController(BloodContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/Appointments
        [HttpGet("donationCenter/{donationCenterID}/{position}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDonationC(int donationCenterID, int position)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            var nextPage = _context.Appointments
                .Where(a => a.DonationCenterID == donationCenterID)
                .OrderBy(b => b.Date)
                .Skip(position)
                .Take(3)
                .ToListAsync();
            return await nextPage;
            //return await _context.Appointments.Where(a => a.DonationCenterID == donationCenterID).ToListAsync();
        }

        [HttpGet("donationCenterApp/{donationCenterID}")]
        public async Task<ActionResult<int>> GetNumAppointmentsByDonationC(int donationCenterID)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            
            return await _context.Appointments.Where(a => a.DonationCenterID == donationCenterID).CountAsync();
        }


        // GET: api/Appointments/userid/{userid}
        [HttpGet("userid/{userID}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByUserID(int userID)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            return await _context.Appointments.Where(a => a.DonorID == userID).ToListAsync();
        }

        [HttpGet("nextAppointment/{userID}")]
        public async Task<ActionResult<Appointment>> GetNextAppointment(int userID)
        {
            if (_context.Appointments == null)
            {
                return NotFound(); 
            }
            List<Appointment> appointments = await _context.Appointments.Where(a => a.DonorID == userID).ToListAsync();
            var sortedList = appointments.OrderBy(o => o.Date).ToList();

            Appointment app = sortedList.Last();
            app.Date = app.Date.AddMonths(1);

            return app;
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'BloodContext.Appointments'  is null.");
            }
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
