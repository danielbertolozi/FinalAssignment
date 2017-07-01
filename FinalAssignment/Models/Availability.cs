using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalAssignment.Models
{
	public class Availability
	{
		[Required]
		public int AvailabilityKey { get; set; }
		[Required]
		[Range(0,6)]
		public int DayOfWeek { get; set; }
		[Required]
		[DataType(DataType.Time)]
		public TimeSpan StartTime { get; set; }
		[Required]
		[DataType(DataType.Time)]
		public TimeSpan EndTime { get; set; }
		[Required]
		public bool IsAllDay { get; set; }
		public virtual Medics Medics { get; set; }
	}
}
