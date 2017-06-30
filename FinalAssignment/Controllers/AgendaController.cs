using System;
using System.Collections.Generic;
using System.Security.Claims;
using FinalAssignment.Data;
using FinalAssignment.ViewModels;
using FinalAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssignment.Controllers
{
	public class AgendaController : Controller
	{
		public AgendaController() { }

		[HttpGet]
		public IActionResult Agenda()
		{
			AgendaViewModel Model = new AgendaViewModel();
			List<CalendarEvent> CalendarEventList = new List<CalendarEvent>();
			EventGenerator Generator = new EventGenerator();
			CalendarEvent Event = Generator.CreateNewEvent("Test","A test event", new DateTime(2017, 06, 29, 08, 30, 00), new DateTime(2017, 06, 29, 10, 00, 00), "Daniel Medic", "Jonat Silveira", new DatabaseContext());
			CalendarEventList.Add(Event);
			Model.CalendarEventList = CalendarEventList;
			return View(Model);
		}
	}
}