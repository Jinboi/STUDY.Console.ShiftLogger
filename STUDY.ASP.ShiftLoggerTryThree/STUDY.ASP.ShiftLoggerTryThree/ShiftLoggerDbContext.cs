using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace STUDY.ASP.ShiftLoggerTryThree
{
    public class ShiftLoggerDbContext : DbContext
    {
        public ShiftLoggerDbContext(DbContextOptions<ShiftLoggerDbContext> options) : base(options) { }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, constraints, etc.
            modelBuilder.Entity<Shift>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(s => s.EmployeeId);
        }
    }
}
