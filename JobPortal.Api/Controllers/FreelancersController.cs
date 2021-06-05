using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.BusinessModels;
using JobPortal.BusinessModels.Freelancers;
using Microsoft.AspNetCore.Identity;
using JobPortal.BusinessModels.General;

namespace JobPortal.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FreelancersController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public FreelancersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		// GET: api/Freelancers
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Freelancer>>> GetFreelancers()
		{
			using (var _context = new JobPortalContext())
			{
				return await _context.Freelancers.AsNoTracking().Include(s => s.User).Include(s => s.Skills).ToListAsync();
			}
		}

		// GET: api/Freelancers/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Freelancer>> GetFreelancer(int id)
		{
			using (var _context = new JobPortalContext())
			{
				var freelancer = await _context.Freelancers.FindAsync(id);

				await _context.Entry(freelancer).Reference(f => f.User).LoadAsync();
				await _context.Entry(freelancer).Collection(f => f.Skills).LoadAsync();

				if (freelancer == null)
				{
					return NotFound();
				}

				return freelancer;
			}
		}

		// PUT: api/Freelancers/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFreelancer(int id, Freelancer freelancer)
		{
			if (id != freelancer.Id)
			{
				return BadRequest();
			}
			using (var _context = new JobPortalContext())
			{
				_context.Entry(freelancer).State = EntityState.Modified;

				try
				{
					await _context.SaveChangesAsync();
					return NoContent();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!FreelancerExists(id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}

		}

		// POST: api/Freelancers
		[HttpPost]
		public async Task<ActionResult<Freelancer>> PostFreelancer(Freelancer freelancer)
		{
			if (!await _roleManager.RoleExistsAsync("Freelancer"))
			{
				await _roleManager.CreateAsync(new IdentityRole("Freelancer"));
			}

			//using (var _context = new JobPortalContext())
			//{
			//	ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == freelancer.User.Id);
			//	await _userManager.AddToRoleAsync(user, "Freelancer");
			//}
			// UNDONE -- Assign role to freelancer

			using (var context = new JobPortalContext())
			{
				ApplicationUser user = await context.Users.FirstOrDefaultAsync(u => u.Id == freelancer.User.Id);
				freelancer.User = user;
				context.Freelancers.Add(freelancer);
				await context.SaveChangesAsync();
			}


			return CreatedAtAction("GetFreelancer", new { id = freelancer.Id }, freelancer);
		}

		// DELETE: api/Freelancers/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Freelancer>> DeleteFreelancer(int id)
		{
			using (var _context = new JobPortalContext())
			{
				var freelancer = await _context.Freelancers.FindAsync(id);
				if (freelancer == null)
				{
					return NotFound();
				}

				await _userManager.RemoveFromRoleAsync(freelancer.User, "Freelancer");
				_context.Freelancers.Remove(freelancer);
				await _context.SaveChangesAsync();

				return freelancer;
			}
		}

		private bool FreelancerExists(int id)
		{
			using (var _context = new JobPortalContext())
			{
				return _context.Freelancers.Any(e => e.Id == id);
			}
		}
	}
}
