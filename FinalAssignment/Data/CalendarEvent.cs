using System;
using FinalAssignment.Models;
namespace FinalAssignment.Data
{
	public class CalendarEvent
	{
		public CalendarEvent(string title, string description, DateTime startTime, DateTime endTime, Medics medic, Patients patient)
		{
			this.Title = title;
			this.Description = description;
			this.StartTime = startTime;
			this.EndTime = endTime;
			this.Medic = medic;
			this.Patient = patient;
		}
		public string Title { get; set; }
		public string Description { get; set; }
		public Medics Medic { get; set; }
		public Patients Patient { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		/* TODO maybe change StartTime and EndTime for Date and Duration */
	}
}
