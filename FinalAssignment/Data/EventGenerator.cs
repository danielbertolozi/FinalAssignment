using System;
using System.Linq;
using FinalAssignment.Models;
namespace FinalAssignment.Data
{
	public class EventGenerator
	{
		public CalendarEvent CreateNewEvent(
			string Title,
			string Description,
			DateTime StartTime,
			DateTime EndTime,
			string MedicName,
			string PatientName,
			DatabaseContext Database
		)
		{
			Medics Medic = this._SearchForMedic(MedicName, Database);
			Patients Patient = this._SearchForPatient(PatientName, Database);
			return new CalendarEvent(Title, Description, StartTime, EndTime, Medic, Patient);
		}

		private Medics _SearchForMedic(string Name, DatabaseContext Database)
		{
			return Database.Medics.Where(t => t.Name == Name).FirstOrDefault();
		}

		private Patients _SearchForPatient(string Name, DatabaseContext Database)
		{
			return Database.Patients.Where(t => t.Name == Name).FirstOrDefault();
		}
	}
}
