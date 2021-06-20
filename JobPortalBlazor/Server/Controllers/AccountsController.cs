using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JobPortalBlazor.Shared;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortalBlazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly SignInManager<AspNetUser> _signInManager;
		private readonly JobPortalDBContext _context;
		private readonly UserManager<AspNetUser> _userManager;
		private readonly RoleManager<AspNetRole> _roleManager;

		public AccountsController(JobPortalDBContext context, UserManager<AspNetUser> userManager, RoleManager<AspNetRole> roleManager, SignInManager<AspNetUser> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		// GET: api/accounts
		[HttpGet]
		public async Task<ActionResult<List<AspNetUser>>> GetUsers()
		{
			return await _context.Users.AsNoTracking().ToListAsync();
		}

		// GET: api/Accounts/{email}
		[HttpGet("{email}")]
		public async Task<ActionResult<AspNetUser>> GetUser(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}

		// POST: api/Accounts
		[HttpPost]
		public async Task<ActionResult<AspNetUser>> PostUser(AspNetUser user)
		{
			var result = await _userManager.CreateAsync(user);
			if (result.Succeeded)
			{
				// UNDONE -- Add to Roles
				//await _userManager.AddToRoleAsync(user, "Client");
				return CreatedAtAction("GetUser", new { id = user.Id }, user);
			}
			else
			{
				return BadRequest();
			}
		}

		// DELETE: api/Accounts/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<AspNetUser>> DeleteAccount(string id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			var roleNames = _roleManager.Roles.ToList().Select(role => role.Name);
			await _userManager.RemoveFromRolesAsync(user, roleNames);

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return user;
		}
	}
}