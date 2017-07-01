using System;
using Microsoft.EntityFrameworkCore;
using FinalAssignment.Models.Mappings;

namespace FinalAssignment.Models
{
	public class DatabaseContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=Database.db");
		}

		public DbSet<Availability> Availability { get; set; }
		public DbSet<Consults> Consults { get; set; }
		public DbSet<Medics> Medics { get; set; }
		public DbSet<Patients> Patients { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new AvailabilityMap(modelBuilder.Entity<Availability>());
			new ConsultsMap(modelBuilder.Entity<Consults>());
			new MedicsMap(modelBuilder.Entity<Medics>());
			new PatientsMap(modelBuilder.Entity<Patients>());

			//modelBuilder.Entity<Consults>().HasOne(t => t.Medic)
			//            .WithMany(t => t.Consults)
			//            .HasForeignKey(t => t.MedicKey);

			//modelBuilder.Entity<Consults>().HasOne(t => t.Patient)
						//.WithMany(t => t.Consults)
						//.HasForeignKey(t => t.PatientKey);
		}

	}
}
