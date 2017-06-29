using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FinalAssignment.ViewModels;
using FinalAssignment.Models;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;

namespace FinalAssignment.Controllers
{
	public class AccountController : Controller
	{
		private DatabaseContext _Context;

		public AccountController (DatabaseContext Context)
		{
			this._Context = Context;
		}

		[HttpGet]
		public IActionResult Login()
		{
			List<SelectListItem> AccountTypes = new List<SelectListItem>();
			AccountTypes.Add(new SelectListItem { Text = "Patient", Value = "P" });
			AccountTypes.Add(new SelectListItem { Text = "Medic", Value = "M" });
			@ViewBag.AccountTypes = AccountTypes;
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
		public async Task<IActionResult> Login(LoginViewModel Model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					using (var Database = new DatabaseContext())
					{
						if (Model.AccountType == 'M')
						{
							Medics Result = Database
								.Medics
								.Where(p => p.Email == Model.Email &&
									   p.Password == Model.Password)
								.FirstOrDefault();

							if (Result == null)
							{
								throw new Exception("User not found");
							}
							var Claims = new List<Claim> { new Claim("Role", "Medic") };
							var ClaimsIdentity = new ClaimsIdentity(Claims);
							var ClaimsPrincipal = new ClaimsPrincipal(ClaimsIdentity);
							await HttpContext.Authentication.SignInAsync("CookieMiddleware", ClaimsPrincipal);
							return RedirectToAction("Index", "Home");
						}
					}
				}
			}
			catch (Exception e)
			{
				@ViewBag.Error = "Could not create account. " + e.Message;
				return View();
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateViewModel Model)
		{
			var ConvertedModel = this._MapViewModel(Model);
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
				@ViewBag.Error = "Could not create account. " + e.Message;
				return View();
			}
			return RedirectToAction("Index", "Home");
		}

		private Object _MapViewModel(CreateViewModel Model)
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

		private Object _MapViewModel(LoginViewModel Model)
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
