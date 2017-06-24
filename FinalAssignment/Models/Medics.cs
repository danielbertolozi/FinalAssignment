using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalAssignment.Models
{
	public class Medics
	{
		public Medics()
		{
		}
		[Required]
		[StringLength(64)]
		public string Name { get; set; }
		[Required]
		[Timestamp]
		[DisplayName("Date of Birth")]
		public DateTime BirthDate { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		[StringLength(64)]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.PhoneNumber)]
		[StringLength(14)]
		public string Phone { get; set; }
		[Required]
		[StringLength(32)]
		public string Address { get; set; }
		[Required]
		[RegularExpression("[mfMF]", ErrorMessage = "Please insert a valid genre value (M-F).")]
		public char Genre { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[StringLength(32, MinimumLength = 8, ErrorMessage = "Please insert a password that is greater than 8 characters.")]
		public string Password { get; set; }
		[Required]
		[Range(0,1)]
		public int Deleted { get; set; }
	}
}
