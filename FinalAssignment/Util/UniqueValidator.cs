using System;
using System.Linq;
using FinalAssignment.Models;
using FluentValidation;

namespace FinalAssignment.Util
{
	public class MedicsUniqueValidator : AbstractValidator<Medics>
	{
		public MedicsUniqueValidator()
		{
			RuleFor(x => x.Email).Must(BeUniqueEmail).WithMessage("Email already exists!");
		}
		private bool BeUniqueEmail (string Email)
		{
			return new DatabaseContext().Medics.FirstOrDefault(t => t.Email == Email) == null;
		}
	}
	public class PatientsUniqueValidator : AbstractValidator<Patients>
	{
		public PatientsUniqueValidator()
		{
			RuleFor(x => x.Email).Must(BeUniqueEmail).WithMessage("Email already exists!");
		}
		private bool BeUniqueEmail(string Email)
		{
			return new DatabaseContext().Patients.FirstOrDefault(t => t.Email == Email) == null;
		}
	}
}
