using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using JobPortal.WebUI.Library.Api.EndPoints;
using JobPortal.WebUI.Models;
using JobPortal.WebUI.Temp;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobPortal.WebUI.Controllers
{
	public class AdminController : Controller
	{
		private readonly ILogger<AdminController> _logger;

		// TODO -- Kashif/Usama, read following comments and implement accordingly.
		// After following methods are implemented, I'll add code for sending emails.

		/*
		 * To delete a message, create an action method. Method should receive Message:Id as parameter
		 * example:
		 * public Task<IActionResult> DeleteMessage(int id)
		 * {
		 *		ContactUsEndPoint endPoint = new ContactUsEndPoint(); // enclose in try catch block and display toasts accordingly
		 *		endPoint.DeleteMessage(TokenStore.Token, id);
		 *	}
		 */

		/*
		 * To mark a message as, create an action method. Method should receive Message:Id as parameter
		 * example:
		 * public Task<IActionResult> ResolveMessage(int id, string recepientEmail, string adminReply)
		 * {
		 *		ContactUsEndPoint endPoint = new ContactUsEndPoint(); // enclose in try catch block and display toasts accordingly
		 *		
		 *		// Here, I'll add code for sending email and will use parameters (recepientEmail,adminReply)
		 *		
		 *		await endPoint.ResolveMessage(TokenStore.Token, id);
		 *	}
		 */

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

		public async Task<IActionResult> ContactUs(ContactForm form)
		{
			ContactUsEndPoint endPoint = new ContactUsEndPoint();

			var list = await endPoint.GetUnResolvedMessages(TokenStore.Token);
			List<ContactForm> displayList = new List<ContactForm>();
			foreach (var item in list)
			{
				displayList.Add(new ContactForm() { Email = item.Email, ID = item.Id, Message = item.Message, Name = item.Name, Subject = item.Subject });
			}
			ViewData["contact-us"] = displayList;
			return View("ContactUs");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}