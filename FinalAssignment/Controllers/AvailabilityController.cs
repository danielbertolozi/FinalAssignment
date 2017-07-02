using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinalAssignment.Data;
using FinalAssignment.Models;
using FinalAssignment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalAssignment.Controllers
{
	public class AvailabilityController : Controller
	{
		private readonly DatabaseContext _Context;
		private readonly UserManager _UserManager;
		public AvailabilityController(DatabaseContext Context, UserManager UserManager)
		{
			this._Context = Context;
			this._UserManager = UserManager;
		}

		[HttpGet]
		public IActionResult Configure()
		{
			AvailabilityViewModel Model = new AvailabilityViewModel();
			List<Availability> List = new List<Availability>();
			List.Add(new Availability());
			List<Availability>[] AvailabilityList = new List<Availability>[7];
			for (int i = 0; i < AvailabilityList.Length; i++)
			{
				AvailabilityList[i] = List;
			}
			Model.AvailabilityList = AvailabilityList;
			return View(Model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var List = new List<SelectListItem>();
			List.Add(new SelectListItem { Text = "Sunday", Value = "0" });
			List.Add(new SelectListItem { Text = "Monday", Value = "1" });
			List.Add(new SelectListItem { Text = "Tuesday", Value = "2" });
			List.Add(new SelectListItem { Text = "Wednesday", Value = "3" });
			List.Add(new SelectListItem { Text = "Thursday", Value = "4" });
			List.Add(new SelectListItem { Text = "Friday", Value = "5" });
			List.Add(new SelectListItem { Text = "Saturday", Value = "6" });
			ViewBag.DaysOfWeek = List;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Availability Model)
		{
			string UserMail = _UserManager.GetUserEmail(this.User);
			try
			{
				if (ModelState.IsValid)
				{
					Model.MedicKey = _UserManager.GetMedicKeyByEmail(UserMail);
					_Context.Add(Model);
					await _Context.SaveChangesAsync();
				}
				return RedirectToAction("Configure", "Availability");
			}
			catch (Exception e)
			{
				Console.Write(e);
				return RedirectToAction("Create", "Availability");
			}
		}
	}
}