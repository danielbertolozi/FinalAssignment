using System.Collections.Generic;
using FinalAssignment.Data;
using FinalAssignment.Models;
using FinalAssignment.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

		public IActionResult Create()
		{
			return View();
		}
	}
}