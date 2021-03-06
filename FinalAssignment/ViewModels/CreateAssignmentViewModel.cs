﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FinalAssignment.Models;

namespace FinalAssignment.ViewModels
{
	public class CreateAssignmentViewModel
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
		[DataType(DataType.Duration)]
		public TimeSpan Duration { get; set; }
		[Required]
		[Range(1, 3)]
		[DisplayName("Consult Type")]
		public int Classification { get; set; }
		public int MedicKey { get; set; }
		public int PatientKey { get; set; }
		[DisplayName("Medic")]
		public string SelectedMedicKey { get; set; }
		[DisplayName("Patient")]
		public string SelectedPatientKey { get; set; }
	}
}
