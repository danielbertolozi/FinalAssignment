using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalAssignment.Models
{
	public class Availability
	{
		public Availability()
		{
		}
		[Required]
		public int AvailabilityKey { get; set; }
		[Required]
		[Range(1,7)]
		public int DayOfWeek { get; set; }
		[Required]
		[RegularExpression("\\d{2}:\\d{2}", ErrorMessage = "Please provide time in the following fashion: [HH:mm]")]
		public string StartTime { get; set; }
		[Required]
		[RegularExpression("\\d{2}:\\d{2}", ErrorMessage = "Please provide time in the following fashion: [HH:mm]")]
		public string EndTime { get; set; }
		[Required]
		[Range(0,1)]
		public int IsAllDay { get; set; }
		public virtual Medics Medics { get; set; }
	}
}
