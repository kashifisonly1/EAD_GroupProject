﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using JobPortal.Api.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobPortal.Api.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}