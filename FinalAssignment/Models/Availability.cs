﻿using System;
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
		[DisplayName("Day of the Week")]
		public int DayOfWeek { get; set; }
		[Required]
		[DataType(DataType.Time)]
		[DisplayName("Start:")]
		public TimeSpan StartTime { get; set; }
		[Required]
		[DataType(DataType.Time)]
		[DisplayName("End:")]
		public TimeSpan EndTime { get; set; }
		[Required]
		[DisplayName("All Day:")]
		public bool IsAllDay { get; set; }
		public int MedicKey { get; set; }
		public virtual Medics Medics { get; set; }
	}
}
