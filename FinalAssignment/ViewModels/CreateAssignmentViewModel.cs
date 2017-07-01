using System;
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
		[DataType(DataType.Time)]
		public TimeSpan Duration { get; set; }
		[Required]
		[Range(1, 3)]
		[DisplayName("Consult Type")]
		public int Classification { get; set; }
		[DisplayName("Medic")]
		public string SelectedMedicId { get; set; }
		[DisplayName("Patient")]
		public string SelectedPatientId { get; set; }
	}
}
