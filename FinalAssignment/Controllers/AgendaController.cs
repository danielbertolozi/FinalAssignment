using System;
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

namespace FinalAssignment.Controllers
{
	public class AgendaController : Controller
	{
		private DatabaseContext _Context;
		private UserManager _UserManager;

		public AgendaController(DatabaseContext Context, UserManager UserManager)
		{
			this._Context = Context;
			this._UserManager = UserManager;
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
			string UserRole = _UserManager.GetUserRole(this.User).Value;
			@ViewBag.AtendeesList = UserRole == "Patient" ? _GetMedicsSelectList() : _GetPatientsSelectList();
			@ViewBag.ClassificationList = _GetClassificationsSelectListItem();
			@ViewBag.UserRole = UserRole;
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateAssignmentViewModel Model)
		{
			/* TODO Handle exception for Consult Type as soon as DB have the trigger ready */
			/* TODO Validate if date isn't previous than today */
			/* TODO Verify meeting time as it isn't working */
			try
			{
				if (ModelState.IsValid)
				{
					Model.MedicKey = this._AttachMedicKey(_Context, Model.SelectedMedicKey);
					Model.PatientKey = this._AttachPatientKey(_Context, Model.SelectedPatientKey);
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
				@ViewBag.Error = "An error occurred.";
				return RedirectToAction("Agenda", "Agenda");
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

		private int _AttachMedicKey(DatabaseContext Context, string PatientKey)
		{
			string Email = this._UserManager.GetUserEmail(this.User);
			if (PatientKey.Length > 0)
			{
				return int.Parse(PatientKey);
			}
			return _Context.Medics.Where(t => t.Email == Email).FirstOrDefault().MedicKey;
		}

		private int _AttachPatientKey(DatabaseContext Context, string PatientKey)
		{
			string Email = this._UserManager.GetUserEmail(this.User);
			if (PatientKey.Length > 0)
			{
				return int.Parse(PatientKey);
			}
			return _Context.Patients.Where(t => t.Email == Email).FirstOrDefault().PatientKey;
		}
	}
}