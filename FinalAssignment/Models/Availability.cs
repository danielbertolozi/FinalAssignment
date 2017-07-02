using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalAssignment.Models
{
	public class Availability
	{
		[Required]
		public int AvailabilityKey { get; set; }
		[Required]
		[Range(0,6)]
		[DisplayName("Select the Day of the Week:")]
		public int DayOfWeek { get; set; }
		[Required]
		[DataType(DataType.Time)]
		[DisplayName("Enter the beginning time:")]
		public TimeSpan StartTime { get; set; }
		[Required]
		[DataType(DataType.Time)]
		[DisplayName("Enter the ending time:")]
		public TimeSpan EndTime { get; set; }
		[Required]
		[DisplayName("Is it an All Day Event?")]
		public bool IsAllDay { get; set; }
		public int MedicKey { get; set; }
		public virtual Medics Medics { get; set; }
	}
}
