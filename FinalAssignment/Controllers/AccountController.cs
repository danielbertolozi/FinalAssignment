using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FinalAssignment.ViewModels;

namespace FinalAssignment.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public List<IActionResult> Create(CreateViewModel Model)
		{
			// TODO for this, create a class that will handle requests, and import it here
		}
	}
}
