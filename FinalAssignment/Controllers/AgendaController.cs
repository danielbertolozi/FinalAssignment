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
using System.Threading.Tasks;

namespace FinalAssignment.Controllers
{
	public class AgendaController : Controller
	{
		private DatabaseContext _Context;
		private List<Medics> _Medics;
		private List<Patients> _Patients;

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
			@ViewBag.AtendeesList = UserRole == "Patient" ? _GetMedicsSelectList() : _GetPatientsSelectList();
			@ViewBag.ClassificationList = _GetClassifications();
			@ViewBag.UserRole = UserRole;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Consults Consult)
		{
			/* TODO Handle exception for Consult Type as soon as DB have the trigger ready */
			/* Validate if date isn't previous than today */
			try
			{
				if (ModelState.IsValid)
				{
					using (var Database = new DatabaseContext())
					{
						Database.Add(Consult);
						await Database.SaveChangesAsync();
					}
					@ViewBag.Success = "The Assignment has been created successfully!";
					return RedirectToAction("Agenda", "Agenda");
				}
			}
			catch (Exception e)
			{
				Console.Write(e);
				@ViewBag.Error = "An error occurred.";
				return RedirectToAction("Agenda", "Agenda");
			}
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

		private List<SelectListItem> _GetMedicsSelectList()
		{
			List<Medics> Medics = this._RetrieveMedicsList();
			this._setMedicsCache(Medics);
			List<SelectListItem> MedicsList = new List<SelectListItem>();
			foreach (Medics Medic in Medics)
			{
				MedicsList.Add(new SelectListItem { Text = Medic.Name, Value = Medic.MedicKey.ToString() });
				/* TODO bug here; it can't hold an Medic object, so a ViewModel is going to be necessary. Hold the value into something like SelectedValueId and then fetch the object accordingly */
			}
			return MedicsList;
		}

		private List<SelectListItem> _GetPatientsSelectList()
		{
			List<Patients> Patients = this._RetrievePatientsList();
			this._setPatientsCache(Patients);
			List<SelectListItem> PatientsList = new List<SelectListItem>();
			foreach (Patients Patient in Patients)
			{
				PatientsList.Add(new SelectListItem { Text = Patient.Name, Value = Patient.PatientKey.ToString() });
			}
			return PatientsList;
		}

		private List<SelectListItem> _GetClassifications()
		{
			List<SelectListItem> ClassificationsList = new List<SelectListItem>();
			ClassificationsList.Add(new SelectListItem { Text = "First Consult", Value = "1" });
			ClassificationsList.Add(new SelectListItem { Text = "Re-consult", Value = "2" });
			ClassificationsList.Add(new SelectListItem { Text = "Routine Consult", Value = "3" });
			return ClassificationsList;
		}

		private void _setMedicsCache(List<Medics> MedicsList)
		{
			this._Medics = MedicsList;
		}

		private void _setPatientsCache(List<Patients> PatientsList)
		{
			this._Patients = PatientsList;
		}

		private List<Medics> _RetrieveMedicsList()
		{
			using (var Database = new DatabaseContext())
			{
				return Database.Medics.ToList();
			}
		}

		private List<Patients> _RetrievePatientsList()
		{
			using (var Database = new DatabaseContext())
			{
				return Database.Patients.ToList();
			}
		}
	}
}