﻿using System;
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
						var Result = this._VerifyLogon(Database, Model);
						if (Result == null)
						{
							throw new Exception("User not found");
						}
						ClaimsPrincipal ClaimsPrincipal = this._GetClaims(Model.AccountType);
						await HttpContext.Authentication.SignInAsync("CookieMiddleware", ClaimsPrincipal);
						return RedirectToAction("Index", "Home");
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

		private Object _VerifyLogon(DatabaseContext Database, LoginViewModel Model)
		{
			if (Model.AccountType == 'M')
			{
				return this._VerifyMedicLogon(Database, Model);
			}
			else if (Model.AccountType == 'P')
			{
				return this._VerifyPatientLogon(Database, Model);
			}
			return null;
		}

		private Medics _VerifyMedicLogon(DatabaseContext Database, LoginViewModel Model)
		{
			Medics Result = Database
								.Medics
								.Where(p => p.Email == Model.Email &&
									   p.Password == Model.Password)
								.FirstOrDefault();
			return Result;
		}

		private Patients _VerifyPatientLogon(DatabaseContext Database, LoginViewModel Model)
		{
			Patients Result = Database
								.Patients
								.Where(p => p.Email == Model.Email &&
									   p.Password == Model.Password)
								.FirstOrDefault();
			return Result;
		}

		private ClaimsPrincipal _GetClaims(char AccountType)
		{
			string Role = AccountType == 'M' ? "Medic" : "Patient";
			var Claims = new List<Claim> { new Claim("Role", Role) };
			var ClaimsIdentity = new ClaimsIdentity(Claims);
			var ClaimsPrincipal = new ClaimsPrincipal(ClaimsIdentity);
			return ClaimsPrincipal;
		}
	}
}
