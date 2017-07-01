using System;
using System.Collections.Generic;
using System.Linq;
using FinalAssignment.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssignment.Data
{
	public class UserManager
	{
		public List<Medics> RetrieveMedicsList()
		{
			using (var Database = new DatabaseContext())
			{
				return Database.Medics.ToList();
			}
		}

		public List<Patients> RetrievePatientsList()
		{
			using (var Database = new DatabaseContext())
			{
				return Database.Patients.ToList();
			}
		}

		public string GetUserEmail(ClaimsPrincipal User)
		{
			return User.Claims.Where(c => c.Type == "UserMail").FirstOrDefault().Value;
		}

		public Medics VerifyMedicLogon(string Email, string Password)
		{
			using (var Database = new DatabaseContext())
			{
				return Database.Medics.Where(p => p.Email == Email && p.Password == Password).FirstOrDefault();
			}
		}

		public Patients VerifyPatientLogon(string Email, string Password)
		{
			using (var Database = new DatabaseContext())
			{
				return Database.Patients.Where(p => p.Email == Email && p.Password == Password).FirstOrDefault();
			}
		}

		public Claim GetUserRole(ClaimsPrincipal User)
		{
			return User.Claims.FirstOrDefault(t => t.Type == "Role");
		}
	}
}
