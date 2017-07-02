using System;
using FinalAssignment.Models;
using FinalAssignment.Data;

namespace FinalAssignment.Util
{
	public class EventManager
	{
		private readonly UserManager _UserManager;
		private readonly Object User;

		public EventManager(string UserMail, UserManager UserManager)
		{
			this._UserManager = UserManager;
			try
			{
				this.User = this._UserManager.RetrieveMedicByEmail(UserMail);
			}
			catch (Exception e)
			{
				Console.Write(e);
			}
			if (this.User == null)
			{
				try
				{
					this.User = this._UserManager.RetrievePatientByEmail(UserMail);
				}
				catch (Exception e)
				{
					Console.Write(e);
				}
			}
			return;

		}
		public EventManager(Medics Medic, UserManager UserManager)
		{
			this._UserManager = UserManager;
			this.User = Medic;
		}
		public EventManager(Patients Patient, UserManager UserManager)
		{
			this._UserManager = UserManager;
			this.User = Patient;
		}

		public void CheckAgainst()
		{
			/* This method should verify against someone else to check if the agendas match */
			return;
		}

		public void ReClassifyConsult()
		{
			/* This method should apply the business rule as defined by the exercise sheet */
		}
	}
}
