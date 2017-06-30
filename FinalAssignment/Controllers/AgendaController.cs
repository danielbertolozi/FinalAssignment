﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using FinalAssignment.Data;
using FinalAssignment.ViewModels;
using FinalAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

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
			string UserRole = User.Claims.Where(c => c.Type == "Role").FirstOrDefault().Value;
			@ViewBag.AtendeesList = UserRole == "Patient" ? _GetMedicsList() : _GetPatientsList();
			@ViewBag.ClassificationList = _GetClassifications();
			return View();
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

		private List<SelectListItem> _GetMedicsList()
		{
			using (DatabaseContext Database = new DatabaseContext())
			{
				List<Medics> Medics = Database.Medics.ToList();
				List<SelectListItem> MedicsList = new List<SelectListItem>();
				foreach (Medics Medic in Medics)
				{
					MedicsList.Add(new SelectListItem { Text = Medic.Name, Value = Medic.MedicKey.ToString() });
				}
				return MedicsList;
			}
		}

		private List<SelectListItem> _GetPatientsList()
		{
			using (DatabaseContext Database = new DatabaseContext())
			{
				List<Patients> Patients = Database.Patients.ToList();
				List<SelectListItem> PatientsList = new List<SelectListItem>();
				foreach (Patients Patient in Patients)
				{
					PatientsList.Add(new SelectListItem { Text = Patient.Name, Value = Patient.PatientKey.ToString() });
				}
				return PatientsList;
			}
		}

		private List<SelectListItem> _GetClassifications()
		{
			List<SelectListItem> ClassificationsList = new List<SelectListItem>();
			ClassificationsList.Add(new SelectListItem { Text = "First Consult", Value = "1" });
			ClassificationsList.Add(new SelectListItem { Text = "Re-consult", Value = "2" });
			ClassificationsList.Add(new SelectListItem { Text = "Routine Consult", Value = "3" });
			return ClassificationsList;
		}
	}
}