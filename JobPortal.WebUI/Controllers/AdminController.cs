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

			categories.Add(new Category { ID = 1, Name = "Graphics" });
			categories.Add(new Category { ID = 2, Name = "Data BAse" });
			ViewData["category"] = categories;
			return View();
		}
		public IActionResult Skill()
		{
			List<Skill> categories = new List<Skill>();

			categories.Add(new Skill { ID = 1, Name = "Graphics" });
			categories.Add(new Skill { ID = 2, Name = "Data BAse" });
			ViewData["skill"] = categories;
			return View();
		}
		public IActionResult User_()
		{
			List<User> users = new List<User>();
			User user = new User { UserID = "1", UserName = "kashif", UserEmail = "email" };
			users.Add(user);
			users.Add(user); users.Add(user); users.Add(user);
			ViewData["user-list"]=users;
			return View("User");
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