﻿﻿using System;
using FinalAssignment.Models;
using FinalAssignment.Data;
using System.Linq;

namespace FinalAssignment.Util
{
	public class EventManager
	{
		private readonly UserManager _UserManager;
		private readonly Patients _PatientAccount;
		private readonly DatabaseContext _Context;

		public EventManager(string UserMail, UserManager UserManager, DatabaseContext Context)
		{
			this._Context = Context;
			this._UserManager = UserManager;
			try
			{
				this._PatientAccount = this._UserManager.RetrievePatientByEmail(UserMail);
			}
			catch (Exception e)
			{
				Console.Write(e);
			}
			return;

		}

		public EventManager(Patients Patient, UserManager UserManager, DatabaseContext Context)
		{
			this._UserManager = UserManager;
			this._PatientAccount = Patient;
		}

		public void CheckAgainst()
		{
			/* This method should verify against someone else to check if the agendas match */
			return;
		}

		public int ReClassifyConsult()
		{
			var PatientKey = this._PatientAccount.PatientKey;
			if (_CheckAgainstFirstConsult(PatientKey))
			{
				return 1;
			}
			if (_CheckAgainstReConsult(PatientKey))
			{
				return 2;
			}
			return 3;
		}

		private bool _CheckAgainstFirstConsult(int PatientKey)
		{
			return _Context.Consults.Where(t => t.PatientKey == PatientKey).ToArray().Length <= 0;
		}

		private bool _CheckAgainstReConsult(int PatientKey)
		{
			return DateTime.Now.Subtract(_Context.Consults.Where(t => t.PatientKey == PatientKey).OrderBy(t => t.Date).LastOrDefault().Date) > new TimeSpan(30, 0, 0, 0); 
		}
	}
}
