using System;
using FinalAssignment.Models;
namespace FinalAssignment.Data
{
	public class CalendarEvent
	{
		public CalendarEvent(string title, string description, DateTime StartTime, DateTime EndTime, Medics medic, Patients patient)
		{
			this.Title = title;
			this.Description = description;
			this.Date = StartTime;
			this.Duration = EndTime.Subtract(StartTime);
			this.Medic = medic;
			this.Patient = patient;
		}
		public string Title { get; set; }
		public string Description { get; set; }
		public Medics Medic { get; set; }
		public Patients Patient { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan Duration { get; set; }
	}
}
