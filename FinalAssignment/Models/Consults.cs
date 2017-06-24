using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalAssignment.Models
{
	public class Consults
	{
		public Consults()
		{
		}
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime DateTime { get; set; }
		[Required]
		[Range(1,3)]
		public int Classification { get; set; }
		public ICollection<Medics> Medics { get; set; }
		public ICollection<Patients> Patients { get; set; }
	}
}
