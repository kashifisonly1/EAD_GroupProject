using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JobPortal.Api.Data;
using JobPortal.BusinessModels;
using JobPortal.BusinessModels.General;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobPortal.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{

		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		// GET: api/accounts
		[HttpGet]
		public async Task<ActionResult<List<ApplicationUser>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}

		// GET: api/Accounts/{email}
		[HttpGet("{email}")]
		public async Task<ActionResult<ApplicationUser>> GetUser(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}

	}
}
