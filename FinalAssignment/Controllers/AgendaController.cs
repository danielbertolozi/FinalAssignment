using System;
using System.Collections.Generic;
using System.Security.Claims;
using FinalAssignment.Data;
using FinalAssignment.ViewModels;
using FinalAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;

namespace FinalAssignment.Controllers
{
	public class AgendaController : Controller
	{
		private DatabaseContext _Context;
		public AgendaController(DatabaseContext Context)
		{
			this._Context = Context;
		}

		[HttpGet]
		public IActionResult Agenda()
		{
			AgendaViewModel Model = new AgendaViewModel();
			List<CalendarEvent> CalendarEventList = this._FetchEventsFromUser();
			Model.CalendarEventList = CalendarEventList;
			return View(Model);
		}

		private List<CalendarEvent> _FetchEventsFromUser()
		{
			String UserName = User.Identity.Name;
			Claim Role = User.Claims.FirstOrDefault(t => t.Type == "Role");
			String RoleType = Role.Value;
			List<Consults> ConsultsList = new List<Consults>();
			using (var Database = _Context)
			{
				if (RoleType == "Medic")
				{
					ConsultsList = Database.Consults.Where(t => t.Medics.Name == UserName).ToList();
				}
				else if  (RoleType == "Patient")
				{
					ConsultsList = Database.Consults.Where(t => t.Patients.Name == UserName).ToList();
				}
				else
				{
					throw new Exception("Could not retrieve User Role");
				}
			}
			return _ConvertConsultToCalendarEvent(ConsultsList);

		}

		private List<CalendarEvent> _ConvertConsultToCalendarEvent(List<Consults> ConsultsList)
		{
			List<CalendarEvent> EventsList = new List<CalendarEvent>();
			foreach (Consults Consult in ConsultsList)
			{
				EventsList.Add(Mapper.Map<CalendarEvent>(Consult));
			}
			return EventsList;
		}
	}
}