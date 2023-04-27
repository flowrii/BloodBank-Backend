using BloodAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Reflection.Emit;

namespace BloodAPI.Data
{
    public class BloodContext : DbContext
    {
        public BloodContext(DbContextOptions<BloodContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<DonationCenter> DonationCenters { get; set; }
        public DbSet<BloodBank> BloodBanks { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Donor>().ToTable("Donor");
            modelBuilder.Entity<DonationCenter>().ToTable("DonationCenter");
            modelBuilder.Entity<BloodBank>().ToTable("BloodBank");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
        }
    }
}
