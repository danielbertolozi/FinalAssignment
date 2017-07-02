using AutoMapper;
using FinalAssignment.Data;
using FinalAssignment.Models;
using FinalAssignment.Util;
using FinalAssignment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace FinalAssignment.Controllers
{
	public class AccountController : Controller
	{
		private readonly DatabaseContext _Context;
		private readonly UserManager _UserManager;

		public AccountController (DatabaseContext Context, UserManager UserManager)
		{
			this._Context = Context;
			this._UserManager = UserManager;
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
						ClaimsPrincipal ClaimsPrincipal = this._GetClaimsPrincipal(Model.AccountType, Model.Email);
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

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.Authentication.SignOutAsync("CookieMiddleware");
			foreach (var Key in HttpContext.Request.Cookies.Keys)
			{
				HttpContext.Response.Cookies.Delete(Key);
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
				return this._UserManager.VerifyMedicLogon(Model.Email, Model.Password);
			}
			else if (Model.AccountType == 'P')
			{
				return this._UserManager.VerifyPatientLogon(Model.Email, Model.Password);
			}
			return null;
		}

		private ClaimsPrincipal _GetClaimsPrincipal(char AccountType, string Email)
		{
			string Role = AccountType == 'M' ? "Medic" : "Patient";
			var Claims = new List<Claim> { new Claim("Role", Role), new Claim("UserMail", Email)};
			ClaimsIdentity ClaimsIdentity = new ClaimsIdentity(Claims, "CustomAuthenticationType"); /* A change in .Net 4.5 requires the string to be passed, https://leastprivilege.com/2012/09/24/claimsidentity-isauthenticated-and-authenticationtype-in-net-4-5/*/
			ClaimsPrincipal ClaimsPrincipal = new ClaimsPrincipal(ClaimsIdentity);
			return ClaimsPrincipal;
		}
	}
}
