using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FinalAssignment.ViewModels;
using FinalAssignment.Models;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalAssignment.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult LoginPatient()
		{
			return View();
		}

		[HttpGet]
		public IActionResult LoginMedic()
		{
			return View();
		}
		
		[HttpGet]
		public IActionResult Create()
		{
			List<SelectListItem> Genres = new List<SelectListItem>();
			Genres.Add(new SelectListItem { Text = "Male", Value = "M" });
			Genres.Add(new SelectListItem { Text = "Female", Value = "F" });
			List<SelectListItem> AccountTypes = new List<SelectListItem>();
			AccountTypes.Add(new SelectListItem { Text = "Patient", Value = "P" });
			AccountTypes.Add(new SelectListItem { Text = "Medic", Value = "M" });
			@ViewBag.Genres = Genres;
			@ViewBag.AccountTypes = AccountTypes;
			return View();
		}

		[HttpPost]
		public Task<IActionResult> LoginPatient(LoginViewModel Model)
		{
			var ConvertedModel = this._MapPatientLoginViewModel(Model);
		}

		[HttpPost]
		public Task<IActionResult> LoginMedic(LoginViewModel Model)
		{
			var ConvertedModel = this._MapMedicLoginViewModel(Model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateViewModel Model)
		{
			var ConvertedModel = this._MapCreateViewModel(Model);
			try {
				if (ModelState.IsValid)
				{
					using (var Database = new DatabaseContext())
					{
						Database.Add(ConvertedModel);
						await Database.SaveChangesAsync();
					}
					return RedirectToAction("Login", "Account");
				}
			}
			catch (Exception e)
			{
				Console.Write(e);
			}
			return RedirectToAction("Index", "Home");
		}

		private Object _MapPatientLoginViewModel(LoginViewModel Model)
		{
			return Mapper.Map<Patients>(Model);
		}

		private Object _MapMedicLoginViewModel(LoginViewModel Model)
		{
			return Mapper.Map<Medics>(Model);
		}

		private Object _MapCreateViewModel(CreateViewModel Model)
		{
			
			if (Model.AccountType == 'M')
			{
				return Mapper.Map<Medics>(Model);
			}
			else
			{
				return Mapper.Map<Patients>(Model);
			}
		}
	}
}
