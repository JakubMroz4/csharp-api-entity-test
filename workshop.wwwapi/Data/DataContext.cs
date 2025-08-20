using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Appointment Key etc.. Add Here
            // Composite key for the join table
            modelBuilder.Entity<Appointment>()
                .HasKey(a => new { a.DoctorId, a.PatientId });

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.DoctorId);

            //TODO: Seed Data Here
            var doc1 = new Doctor { Id = 1, FullName = "Adam Doe" };
            var doc2 = new Doctor { Id = 2, FullName = "Florian Frank" };
            var doc3 = new Doctor { Id = 3, FullName = "Bob Bagel" };
            var doc4 = new Doctor { Id = 4, FullName = "Robert Bank" };
            var doc5 = new Doctor { Id = 5, FullName = "Leonardo Gonzalez" };

            var pat1 = new Patient { Id = 1, FullName = "Patrick" };
            var pat2 = new Patient { Id = 2, FullName = "Spongebob" };
            var pat3 = new Patient { Id = 3, FullName = "Sandy" };
            var pat4 = new Patient { Id = 4, FullName = "Mr Crabs" };
            var pat5 = new Patient { Id = 5, FullName = "Squidward" };

            modelBuilder.Entity<Doctor>().HasData(
                doc1, doc2, doc3, doc4, doc5
                );

            modelBuilder.Entity<Patient>().HasData(
                pat1, pat2, pat3, pat4, pat5
                );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 1, PatientId = 1},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 2, PatientId = 1},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 3, PatientId = 1},

                new Appointment { Booking = DateTime.UtcNow, DoctorId = 2, PatientId = 2},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 3, PatientId = 2},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 4, PatientId = 2},

                new Appointment { Booking = DateTime.UtcNow, DoctorId = 3, PatientId = 3},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 4, PatientId = 3},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 5, PatientId = 3},

                new Appointment { Booking = DateTime.UtcNow, DoctorId = 4, PatientId = 4},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 5, PatientId = 4},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 1, PatientId = 4},

                new Appointment { Booking = DateTime.UtcNow, DoctorId = 5, PatientId = 5},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 1, PatientId = 5},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 2, PatientId = 5}
                );
        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
