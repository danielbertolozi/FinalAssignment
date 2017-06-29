using System;
using System.Collections.Generic;
using FinalAssignment.Models;

namespace FinalAssignment.Data
{
	public class DbInitializer
	{
		enum defaultRoles
		{
			Medic, Patient
		}
		public async static void Initialize (DatabaseContext Context)
		{
			await Context.Database.EnsureCreatedAsync();
			List<Medics> MedicsList = _GenerateMedicsList();
			List<Patients> PatientsList = _GeneratePatientsList();
			foreach (var Medic in MedicsList)
			{
				Context.Add(Medic);
			}
			foreach (var Patient in PatientsList)
			{
				Context.Add(Patient);
			}
			await Context.SaveChangesAsync();
		}

		private static List<Medics> _GenerateMedicsList ()
		{
			List<Medics> MedicsList = new List<Medics>();
			MedicsList.Add(new Medics
			{
				Address = "Address 01",
				Availability = new List<Availability>(),
				BirthDate = new DateTime(1980, 06, 12),
				Consults = new List<Consults>(),
				Deleted = 0,
				Email = "danielbertolozi@gmail.com",
				Genre = 'M',
				Name = "Daniel Medic",
				Password = "12345678",
				Phone = "(51) 12931293"
			});
			MedicsList.Add(new Medics
			{
				Address = "Address 02",
				Availability = new List<Availability>(),
				BirthDate = new DateTime(1998, 02, 03),
				Consults = new List<Consults>(),
				Deleted = 0,
				Email = "Medic02@System.com",
				Genre = 'F',
				Name = "Medic 02",
				Password = "12345678",
				Phone = "(51) 12931293"
			});
			return MedicsList;
		}

		private static List<Patients> _GeneratePatientsList ()
		{
			List<Patients> PatientsList = new List<Patients>;
			PatientsList.Add(new Patients
			{
				Address = "Address 01",
				BirthDate = new DateTime(1996, 08, 27),
				Consults = new List<Consults>(),
				Deleted = 0,
				Email = "danielbertolozi@gmail.com",
				Genre = 'M',
				Name = "Daniel Bertolozi",
				Password = "12345678",
				Phone = "(51) 12931293",
			});
			PatientsList.Add(new Patients
			{
				Address = "Address 02",
				BirthDate = new DateTime(1990, 09, 12),
				Consults = new List<Consults>(),
				Deleted = 0,
				Email = "Jonats@gmail.com",
				Genre = 'M',
				Name = "Jonat Silveira",
				Password = "12345678",
				Phone = "(51) 12931293",
			});
			return PatientsList;
		}
	}
}