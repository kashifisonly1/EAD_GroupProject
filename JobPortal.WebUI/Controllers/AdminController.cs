using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JobPortal.WebUI.Areas.Identity.Data;
using JobPortal.WebUI.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobPortal.WebUI.Controllers
{
	//[Authorize]
	public class AdminController : Controller
	{
		private readonly ILogger<AdminController> _logger;
		private readonly UserManager<ApplicationUser> _userManager;
		public AdminController(ILogger<AdminController> logger, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_userManager = userManager;

		}
		public IActionResult Index()
        {
			return View();	
		}
        public async Task<IActionResult> Index(UserManager<ApplicationUser> userManager)
        {

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //User dummy = new User { UserID = 1, UserEmail = "xamimran8991@gmail.com", UserName = "Usama Imran", isAdmin = false, RoleName = "Moderator" };


            return View("User", currentUser);
        }
        public IActionResult Category()
		{
			List<Category> categories = new List<Category>();

			categories.Add(new Category { ID = 1, PID = 001, Name = "Graphics", Description = "Here You can show your cretivity" });
			categories.Add(new Category { ID = 2, PID = 010, Name = "Data BAse", Description = "Save Data is much easier then ever before" });
			ViewData["category"] = categories;
			return View();
		}
		public IActionResult Users()
		{
			return View();
		}

		public IActionResult ContactUs(ContactForm form)
		{
			List<ContactForm> list = new List<ContactForm>();
			
			form = new ContactForm { Email = "xamimran8991@gmail.com", Subject = "Client Mis behaviour", Name = "Usama", Message = "I want to report kashif" };
			list.Add(form);
			ViewData["Contact-us"] = list;
			return View("ContactUs");
		}
		public IActionResult AddCategory(Category category)
        {

			return View ();
        }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}