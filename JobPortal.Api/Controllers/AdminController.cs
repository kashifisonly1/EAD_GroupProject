using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JobPortal.BusinessModels;
using JobPortal.DataManager.Data;
using JobPortal.WebUI.Library.Api.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobPortal.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		// GET: api/<AdminController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}


	}
}
