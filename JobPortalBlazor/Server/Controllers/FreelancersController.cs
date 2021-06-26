using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortalBlazor.Shared;
using Microsoft.AspNetCore.Identity;

namespace JobPortalBlazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FreelancersController : ControllerBase
	{
		private readonly JobPortalDBContext _context;
		private readonly UserManager<AspNetUser> _userManager;


		public FreelancersController(JobPortalDBContext context, UserManager<AspNetUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: api/Freelancers
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Freelancer>>> GetFreelancers()
		{
			return await _context.Freelancers
				.Include(f => f.User)
				.ToListAsync();
		}

		// GET: api/Freelancers/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Freelancer>> GetFreelancer(int id)
		{
			var freelancer = await _context.Freelancers.FindAsync(id);

			if (freelancer == null)
			{
				return NotFound();
			}
			await _context.Entry(freelancer).Reference(f => f.User).LoadAsync();

			return freelancer;
		}

		// PUT: api/Freelancers/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFreelancer(int id, Freelancer freelancer)
		{
			if (id != freelancer.Id)
			{
				return BadRequest();
			}

			_context.Entry(freelancer).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
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

			return NoContent();
		}

		// POST: api/Freelancers
		[HttpPost]
		public async Task<ActionResult<Freelancer>> PostFreelancer(Freelancer freelancer)
		{
			AspNetUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == freelancer.UserId);
			freelancer.User = user;
			_context.Freelancers.Add(freelancer);
			await _context.SaveChangesAsync();

			await _userManager.RemoveFromRoleAsync(user, "Client");
			await _userManager.AddToRoleAsync(user, "Freelancer");

			return CreatedAtAction("GetFreelancer", new { id = freelancer.Id }, freelancer);
		}

		// DELETE: api/Freelancers/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFreelancer(int id)
		{
			var freelancer = await _context.Freelancers.FindAsync(id);
			if (freelancer == null)
			{
				return NotFound();
			}

			_context.Freelancers.Remove(freelancer);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool FreelancerExists(int id)
		{
			return _context.Freelancers.Any(e => e.Id == id);
		}
	}
}
