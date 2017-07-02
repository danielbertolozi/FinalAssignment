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
using System.Threading.Tasks;
using System;

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

		[Authorize]
		[HttpGet]
		public IActionResult Configure()
		{
			AvailabilityViewModel Model = new AvailabilityViewModel();
			string UserMail = this._UserManager.GetUserEmail(this.User);
			int MedicKey = this._UserManager.GetMedicKeyByEmail(UserMail);
			List<Availability>[] AvailabilityList = _GetEventsListFromUser(MedicKey);
			Model.AvailabilityList = AvailabilityList;
			return View(Model);
		}

		[Authorize]
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

		[Authorize]
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

		private List<Availability>[] _GetEventsListFromUser(int MedicKey)
		{
			List<Availability>[] AvailabilityListArray = new List<Availability>[7];
			AvailabilityListArray = this._InitializeAvailabilityList(AvailabilityListArray);
			List<Availability> AvailabilityList = this._GetEventsFromUser(MedicKey);
			int AvailabilityDay;
			foreach (var Event in AvailabilityList)
			{
				AvailabilityDay = Event.DayOfWeek;
				AvailabilityListArray[AvailabilityDay].Add(Event);
			}
			return AvailabilityListArray;
		}

		private List<Availability> _GetEventsFromUser(int MedicKey)
		{
			return this._Context.Availability.Where(x => x.MedicKey == MedicKey).ToList();
		}

		private List<Availability>[] _InitializeAvailabilityList(List<Availability>[] AvailabilityList)
		{
			for (int i = 0; i < AvailabilityList.Length; i++)
			{
				AvailabilityList[i] = new List<Availability>();
			}
			return AvailabilityList;
		}
	}
}