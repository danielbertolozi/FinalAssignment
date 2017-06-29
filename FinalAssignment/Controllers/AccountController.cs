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
		public async Task<IActionResult> Create(CreateViewModel Model)
		{
			var ConvertedModel = this._MapViewModelToModel(Model);
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

		private Object _MapViewModelToModel(CreateViewModel Model)
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
