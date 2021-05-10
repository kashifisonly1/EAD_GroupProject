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
		public IActionResult User_()
		{
			return View();
		}
		
		public IActionResult ContactUs(ContactForm form)
		{
			List<ContactForm> list = new List<ContactForm>();
			form = new ContactForm {ID=1, Email = "xamimran8991@gmail.com", Subject = "Client Mis behaviour", Name = "Usama", Message = "I want to report kashif"};
			list.Add(form);
			
			ViewData["contact-us"]=list;
			return View("ContactUs");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}