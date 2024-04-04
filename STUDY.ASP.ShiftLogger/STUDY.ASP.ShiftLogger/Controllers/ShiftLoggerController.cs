using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace ShiftLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly ShiftLoggerDbContext _context;

        public ShiftsController(ShiftLoggerDbContext context)
        {
            _context = context;
        }

        // Models
        public class Shift
        {
            public int Id { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public int EmployeeId { get; set; }
            public Employee Employee { get; set; }
        }

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        // DbContext
        public class ShiftLoggerDbContext : DbContext
        {
            public ShiftLoggerDbContext(DbContextOptions<ShiftLoggerDbContext> options) : base(options) { }

            public DbSet<Shift> Shifts { get; set; }
            public DbSet<Employee> Employees { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Shift>()
                    .HasOne(s => s.Employee)
                    .WithMany()
                    .HasForeignKey(s => s.EmployeeId);
            }
        }

        // API Controller Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
        {
            return await _context.Shifts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        [HttpPost]
        public async Task<ActionResult<Shift>> PostShift(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShift), new { id = shift.Id }, shift);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShift(int id, Shift shift)
        {
            if (id != shift.Id)
            {
                return BadRequest();
            }

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.Id == id);
        }
    }
}
