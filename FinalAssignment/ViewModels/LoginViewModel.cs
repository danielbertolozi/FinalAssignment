using System;
using System.ComponentModel.DataAnnotations;

namespace FinalAssignment.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		[StringLength(64)]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[StringLength(32, MinimumLength = 8, ErrorMessage = "Please insert a password that is greater than 8 characters.")]
		public string Password { get; set; }
		[Required]
		[RegularExpression("[mpMP]")]
		[DisplayName("Account Type")]
		public char AccountType { get; set; }
	}
}
