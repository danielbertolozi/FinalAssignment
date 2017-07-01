using FinalAssignment.Data;
using FinalAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssignment.Controllers
{
	public class AvailabilityController : Controller
	{
		private readonly DatabaseContext _Context;
		private readonly UserManager _UserManager;
		public AvailabilityController(DatabaseContext Context, UserManager UserManager)
		{
			this._Context = Context;
			this._UserManager = UserManager;
		}

		[HttpGet]
		public IActionResult Configure()
		{
			return View();
		}
	}
}