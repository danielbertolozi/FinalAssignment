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
using Microsoft.EntityFrameworkCore;
using FinalAssignment.Util;

namespace FinalAssignment.Controllers
{
	public class AgendaController : Controller
	{
		private readonly DatabaseContext _Context;
		private readonly UserManager _UserManager;
		private readonly EventManager _EventManager;

		public AgendaController(DatabaseContext Context, UserManager UserManager)
		{
			this._Context = Context;
			this._UserManager = UserManager;
			string UserMail = _UserManager.GetUserEmail(this.User);
			this._EventManager = new EventManager(UserMail, new UserManager());
		}

		[HttpGet]
		public IActionResult Agenda()
		{
			AgendaViewModel Model = this._LoadAgendaData();
			return View(Model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			this._LoadCreateData();
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateAssignmentViewModel Model)
		{
			if (Model.Date.CompareTo(DateTime.Now) < 0)
			{
				@ViewBag.Error = "Please insert a date ahead from today.";
				this._LoadCreateData();
				return View(Model);
			}
			int NewClassification = this._EventManager.ReClassifyConsult();
			if (NewClassification != Model.Classification)
			{
				Model.Classification = NewClassification;
				@ViewBag.Error = "The consult had to be reclassified due to constraints.";
			}
			try
			{
				if (ModelState.IsValid)
				{
					Model.MedicKey = this._AttachMedicKey(Model.SelectedMedicKey);
					Model.PatientKey = this._AttachPatientKey(Model.SelectedPatientKey);
					Consults Consult = Mapper.Map<Consults>(Model);
					_Context.Add(Consult);
					_Context.SaveChanges();
					@ViewBag.Success = "The Assignment has been created successfully!";
					return RedirectToAction("Agenda", "Agenda");
				}
			}
			catch (Exception e)
			{
				Console.Write(e);
				@ViewBag.Error = "An internal error occurred.";
				this._LoadCreateData();
				return View(Model);
			}
			return View();
		}

		private List<Consults> _FetchEventsFromUser()
		{
			String UserMail = this._UserManager.GetUserEmail(this.User);
			Claim Role = this._UserManager.GetUserRole(this.User);
			String RoleType = Role.Value;
			List<Consults> ConsultsList = new List<Consults>();
			if (RoleType == "Medic")
			{
				ConsultsList = _Context.Consults.Where(t => t.Medic.Email == UserMail).Include(t => t.Medic).Include(t => t.Patient).ToList();
			}
			else if  (RoleType == "Patient")
			{
				ConsultsList = _Context.Consults.Where(t => t.Patient.Email == UserMail).Include(t => t.Medic).Include(t => t.Patient).ToList();
			}
			else
			{
				throw new Exception("Could not retrieve User Role");
			}
			return ConsultsList;
		}

		private List<SelectListItem> _GetMedicsSelectList()
		{
			List<Medics> Medics = this._UserManager.RetrieveMedicsList();
			List<SelectListItem> MedicsList = new List<SelectListItem>();
			foreach (Medics Medic in Medics)
			{
				MedicsList.Add(new SelectListItem { Text = Medic.Name, Value = Medic.MedicKey.ToString() });
			}
			return MedicsList;
		}

		private List<SelectListItem> _GetPatientsSelectList()
		{
			List<Patients> Patients = this._UserManager.RetrievePatientsList();
			List<SelectListItem> PatientsList = new List<SelectListItem>();
			foreach (Patients Patient in Patients)
			{
				PatientsList.Add(new SelectListItem { Text = Patient.Name, Value = Patient.PatientKey.ToString() });
			}
			return PatientsList;
		}

		private List<SelectListItem> _GetClassificationsSelectListItem()
		{
			List<SelectListItem> ClassificationsList = new List<SelectListItem>
			{
				new SelectListItem { Text = "First Consult", Value = "1" },
				new SelectListItem { Text = "Re-consult", Value = "2" },
				new SelectListItem { Text = "Routine Consult", Value = "3" }
			};
			return ClassificationsList;
		}

		private int _AttachMedicKey(string PatientKey)
		{
			string Email = this._UserManager.GetUserEmail(this.User);
			if (PatientKey.Length > 0)
			{
				return int.Parse(PatientKey);
			}
			return _UserManager.GetMedicKeyByEmail(Email);
		}

		private int _AttachPatientKey(string PatientKey)
		{
			string Email = this._UserManager.GetUserEmail(this.User);
			if (PatientKey.Length > 0)
			{
				return int.Parse(PatientKey);
			}
			return _UserManager.GetPatientKeyByEmail(Email);
		}

		private AgendaViewModel _LoadAgendaData()
		{
			AgendaViewModel Model = new AgendaViewModel();
			List<Consults> ConsultsList = this._FetchEventsFromUser();
			Model.ConsultsList = ConsultsList;
			return Model;
		}

		private void _LoadCreateData()
		{
			string UserRole = _UserManager.GetUserRole(this.User).Value;
			@ViewBag.AtendeesList = UserRole == "Patient" ? _GetMedicsSelectList() : _GetPatientsSelectList();
			@ViewBag.ClassificationList = _GetClassificationsSelectListItem();
			@ViewBag.UserRole = UserRole;
		}
	}
}