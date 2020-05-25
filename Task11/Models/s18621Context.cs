using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Task11.Models
{
    public class s18621Context : DbContext
    {
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }
        public s18621Context(DbContextOptions<s18621Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Doctor> doctors = new List<Doctor>();
            doctors.Add(new Doctor { IdDoctor = 1, FirstName = "Kamil", LastName = "Firek", Email = "kefirek@o2.com" });
            doctors.Add(new Doctor { IdDoctor = 2, FirstName = "Andrzej", LastName = "Jakis", Email = "ajax@gmail.com" });
            doctors.Add(new Doctor { IdDoctor = 3, FirstName = "Michal", LastName = "Jest", Email = "oewf@interia.pl" });
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_PK");
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.HasData(doctors);
            });

            List<Patient> patients = new List<Patient>();
            patients.Add(new Patient { FirstName = "Michal", LastName = "Kowalski", IdPatient = 1, BirthDate = DateTime.Parse("10-01-1990")});
            patients.Add(new Patient { FirstName = "Adam", LastName = "Nowak", IdPatient = 2, BirthDate = DateTime.Parse("02-12-1952") });
            patients.Add(new Patient { FirstName = "Piotr", LastName = "Oten", IdPatient = 3, BirthDate = DateTime.Parse("14-04-2000") });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("Patient_PK");
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.BirthDate).IsRequired();
                entity.HasData(patients);
            });

            List<Medicament> medicament = new List<Medicament>();
            medicament.Add(new Medicament { IdMedicament = 1, Name = "Octanisept", Description = "Disinfect wound", Type = "liquid" });
            medicament.Add(new Medicament { IdMedicament = 2, Name = "Paracetamol", Description = "For really strong pain", Type = "painkiller" });
            medicament.Add(new Medicament { IdMedicament = 3, Name = "Apap", Description = "Noc", Type = "painkiller" });
            modelBuilder.Entity<Medicament>(e => {
                e.HasKey(e => e.IdMedicament).HasName("Medicament_PK");
                e.Property(e => e.Name).HasMaxLength(100).IsRequired();
                e.Property(e => e.Description).HasMaxLength(100).IsRequired();
                e.Property(e => e.Type).HasMaxLength(100).IsRequired();
                e.HasData(medicament);
            });

            List<Prescription> prescriptions = new List<Prescription>();
            prescriptions.Add(new Prescription { IdPrescription = 1, Date = DateTime.Now.AddDays(15), DueDate = DateTime.Now.AddDays(52), IdPatient = 3, IdDoctor = 1 });
            prescriptions.Add(new Prescription { IdPrescription = 2, Date = DateTime.Now.AddDays(122), DueDate = DateTime.Now.AddDays(52), IdPatient = 1, IdDoctor = 2 });
            prescriptions.Add(new Prescription { IdPrescription = 3, Date = DateTime.Now.AddDays(22), DueDate = DateTime.Now.AddDays(31), IdPatient = 2, IdDoctor = 3 });
            modelBuilder.Entity<Prescription>(e =>
            {
                e.HasKey(e => e.IdPrescription).HasName("Prescription_PK");
                e.Property(e => e.Date).IsRequired();
                e.Property(e => e.DueDate).IsRequired();
                e.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(e => e.IdDoctor).HasConstraintName("Prescription-Doctor");
                e.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(e => e.IdPatient).HasConstraintName("Prescription-Patient");
                e.HasData(prescriptions);
            });
            List<PrescriptionMedicament> prescripMecidaments = new List<PrescriptionMedicament>();
            prescripMecidaments.Add(new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 3, Dose = 4, Details = "Once a day" });
            prescripMecidaments.Add(new PrescriptionMedicament { IdMedicament = 3, IdPrescription = 2, Dose = 15, Details = "Twice a day" });
            prescripMecidaments.Add(new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 1, Dose = 12, Details = "Once a week" });
            modelBuilder.Entity<PrescriptionMedicament>(e =>
            {
                e.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                e.Property(e => e.Details).HasMaxLength(100).IsRequired();
                e.HasOne(e => e.Prescription).WithMany(e => e.PrescriptionMedicament).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(e => e.IdPrescription).HasConstraintName("Prescription_Prescription_Medicament");
                e.HasOne(e => e.Medicament).WithMany(e => e.PrescriptionMedicament).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(e => e.IdMedicament).HasConstraintName("Medicament-Prescription_Medicament");
                e.HasData(prescripMecidaments);
            });
        }
    }
}
