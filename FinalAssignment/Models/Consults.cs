using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalAssignment.Models
{
	public class Consults
	{
		[Required]
		public int ConsultKey { get; set; }
		[Required]
		[MaxLength(32)]
		public string Title { get; set; }
		[MaxLength(128)]
		public string Description { get; set; }
		[Required]
		[DataType(DataType.Date)]
		[DisplayName("Date")]
		public DateTime Date { get; set; }
		[Required]
		[DataType(DataType.Time)]
		public TimeSpan Duration { get; set; }
		[Required]
		[Range(1,3)]
		public int Classification { get; set; }
		public virtual Medics Medic { get; set; }
		public virtual Patients Patient { get; set; }
	}
}
