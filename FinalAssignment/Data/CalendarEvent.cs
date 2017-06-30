using System;
namespace FinalAssignment.Data
{
	public class CalendarEvent
	{
		public CalendarEvent(string title, string description, DateTime startTime, DateTime endTime)
		{
			this.Title = title;
			this.Description = description;
			this.StartTime = startTime;
			this.EndTime = endTime;
		}
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}
