﻿using Microsoft.EntityFrameworkCore;
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
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 1, PatientId = 1, Type = enums.AppointmentTypeEnum.InPerson},
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 2, PatientId = 1, Type = enums.AppointmentTypeEnum.InPerson },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 3, PatientId = 1, Type = enums.AppointmentTypeEnum.InPerson },

                new Appointment { Booking = DateTime.UtcNow, DoctorId = 2, PatientId = 2, Type = enums.AppointmentTypeEnum.Online },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 3, PatientId = 2, Type = enums.AppointmentTypeEnum.Online },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 4, PatientId = 2, Type = enums.AppointmentTypeEnum.Online },

                new Appointment { Booking = DateTime.UtcNow, DoctorId = 3, PatientId = 3, Type = enums.AppointmentTypeEnum.InPerson },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 4, PatientId = 3, Type = enums.AppointmentTypeEnum.InPerson },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 5, PatientId = 3, Type = enums.AppointmentTypeEnum.InPerson },

                new Appointment { Booking = DateTime.UtcNow, DoctorId = 4, PatientId = 4, Type = enums.AppointmentTypeEnum.Online },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 5, PatientId = 4, Type = enums.AppointmentTypeEnum.Online },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 1, PatientId = 4, Type = enums.AppointmentTypeEnum.Online },

                new Appointment { Booking = DateTime.UtcNow, DoctorId = 5, PatientId = 5, Type = enums.AppointmentTypeEnum.InPerson },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 1, PatientId = 5, Type = enums.AppointmentTypeEnum.InPerson },
                new Appointment { Booking = DateTime.UtcNow, DoctorId = 2, PatientId = 5, Type = enums.AppointmentTypeEnum.InPerson }
                );

            // prescpritions and medicine config & seeder

            modelBuilder.Entity<PrescribedMedicine>()
                .HasKey(a => new { a.MedicineId, a.PrescriptionId });

            modelBuilder.Entity<PrescribedMedicine>()
                .HasOne(pm => pm.Medicine)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(pm => pm.MedicineId);

            modelBuilder.Entity<PrescribedMedicine>()
                .HasOne(pm => pm.Prescription)
                .WithMany(m => m.Medicines)
                .HasForeignKey(pm => pm.PrescriptionId);

            var med1 = new Medicine { Id = 1, Name = "paracetamol" };
            var med2 = new Medicine { Id = 2, Name = "ibuprofen" };

            var prescription1 = new Prescription { Id = 1, AppointmentId = 1 };
            var prescription2 = new Prescription { Id = 2, AppointmentId = 2 };
            var prescription3 = new Prescription { Id = 3, AppointmentId = 3 };
            var prescription4 = new Prescription { Id = 4, AppointmentId = 4 };
            var prescription5 = new Prescription { Id = 5, AppointmentId = 5 };

            modelBuilder.Entity<Medicine>().HasData(
                med1, med2
                );

            modelBuilder.Entity<Prescription>().HasData(
                prescription1, prescription2, prescription3, prescription4, prescription5
                );

            modelBuilder.Entity<PrescribedMedicine>().HasData(
                new PrescribedMedicine { PrescriptionId = 1, MedicineId = 1, Quantity = 5, Notes = "3 every day"},
                new PrescribedMedicine { PrescriptionId = 2, MedicineId = 2, Quantity = 5, Notes = "3 every day" },
                new PrescribedMedicine { PrescriptionId = 3, MedicineId = 1, Quantity = 5, Notes = "3 every day" },
                new PrescribedMedicine { PrescriptionId = 4, MedicineId = 2, Quantity = 5, Notes = "3 every day" },
                new PrescribedMedicine { PrescriptionId = 5, MedicineId = 1, Quantity = 5, Notes = "3 every day" }
                );
        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescribedMedicine> PrescriptionsMedicines { get; set;}
    }
}
