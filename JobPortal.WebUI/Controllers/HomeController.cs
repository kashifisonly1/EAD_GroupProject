using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using JobPortal.BusinessModels;
using JobPortal.WebUI.Library.Api.EndPoints;
using JobPortal.WebUI.Library.Api.Models;
using JobPortal.WebUI.Models;
using JobPortal.WebUI.Temp;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobPortal.WebUI.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult Action()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Contact()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Contact(ContactForm form)
		{
			// UNDONE -- Error on ContactSubmit (needs resolution by front-end)

			if (ModelState.IsValid)
			{
				ContactUsModel model = new ContactUsModel()
				{
					Email = form.Email,
					Name = form.Name,
					Subject = form.Subject,
					Message = form.Message
				};

				AdminEndPoints points = new AdminEndPoints();

				var recieved = await points.GetSomethingFromAPi(TokenStore.Token);

				var contactModel = await points.GetSomeObjectFromAPi(TokenStore.Token);
				// TODO -- send token
				//await points.PostContactUsMessage(TokenStore.Token, model);


			}
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
