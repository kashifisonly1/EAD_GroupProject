using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortalBlazor.Shared;

namespace JobPortalBlazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GigsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public GigsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/Gigs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Gig>>> GetGigs()
		{
			return await _context.Gigs
				.Include(g => g.Category)
				.Include(g => g.Freelancer)
				.ThenInclude(g => g.User)
				.ToListAsync();
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Gig>>> GetMyGigs(String userID)
        {
			return await _context.Gigs
				.Include(g => g.Category)
				.Include(g => g.Freelancer)
				.ThenInclude(g => g.User)
				.ToListAsync();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Gig>>> GetGigsByCategory(int catID)
		{
			return await _context.Gigs
				.Include(g => g.Category)
				.Include(g => g.Freelancer)
				.ThenInclude(g => g.User)
				.ToListAsync();
		}
		// GET: api/Gigs/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Gig>> GetGig(int id)
		{
			var gig = await _context.Gigs.FindAsync(id);

			if (gig == null)
			{
				return NotFound();
			}

			await _context.Entry(gig).Reference(g => g.Category).LoadAsync();

			await _context.Entry(gig).Reference(g => g.Freelancer).LoadAsync();

			return gig;
		}

		// PUT: api/Gigs/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutGig(int id, Gig gig)
		{
			if (id != gig.Id)
			{
				return BadRequest();
			}

			_context.Entry(gig).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GigExists(id))
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

		// POST: api/Gigs
		[HttpPost]
		public async Task<ActionResult<Gig>> PostGig(Gig gig)
		{
			_context.Gigs.Add(gig);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetGig", new { id = gig.Id }, gig);
		}

		// DELETE: api/Gigs/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGig(int id)
		{
			var gig = await _context.Gigs.FindAsync(id);
			if (gig == null)
			{
				return NotFound();
			}

			_context.Gigs.Remove(gig);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool GigExists(int id)
		{
			return _context.Gigs.Any(e => e.Id == id);
		}
	}
}
