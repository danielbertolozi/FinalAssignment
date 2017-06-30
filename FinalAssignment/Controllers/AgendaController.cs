using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssignment.Controllers
{
	public class AgendaController : Controller
	{
		public AgendaController() { }

		[HttpGet]
		public IActionResult Agenda()
		{
			return View();
		}
	}
}