using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using JobPortal.WebUI.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobPortal.WebUI.Controllers
{
	//[Authorize]
	public class AdminController : Controller
	{
		private readonly ILogger<AdminController> _logger;

		public AdminController(ILogger<AdminController> logger)
		{
			_logger = logger;
		}
		public IActionResult Index()
		{
			
			return View();
		}
		public IActionResult Category()
		{
			return View();
		}
		public IActionResult User()
		{
			return View();
		}
		
		public IActionResult ContactUs(ContactForm form)
		{
			form = new ContactForm { Email = "xamimran8991@gmail.com", Subject = "Client Mis behaviour", Name = "Usama", Message = "I want to report kashif"};
			
			return View("ContactUs",form);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}