﻿using System;
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
			List<Consults> ConsultsList = this._FetchEventsFromUser();
			Model.ConsultsList = ConsultsList;
			return View(Model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
			/* TODO methods to fetch dropdown infos */
		}

		private List<Consults> _FetchEventsFromUser()
		{
			String UserName = User.Identity.Name;
			Claim Role = User.Claims.FirstOrDefault(t => t.Type == "Role");
			String RoleType = Role.Value;
			List<Consults> ConsultsList = new List<Consults>();
			using (var Database = new DatabaseContext())
			{
				if (RoleType == "Medic")
				{
					ConsultsList = Database.Consults.Where(t => t.Medic.Name == UserName).ToList();
				}
				else if  (RoleType == "Patient")
				{
					ConsultsList = Database.Consults.Where(t => t.Patient.Name == UserName).ToList();
				}
				else
				{
					throw new Exception("Could not retrieve User Role");
				}
			}
			return ConsultsList;

		}
	}
}