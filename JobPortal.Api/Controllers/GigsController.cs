using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.BusinessModels;
using JobPortal.BusinessModels.Gigs;

namespace JobPortal.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GigsController : ControllerBase
	{
		private readonly JobPortalContext _context;

		public GigsController(JobPortalContext context)
		{
			_context = context;
		}

		// GET: api/Gigs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Gig>>> GetGigs()
		{
			return await _context.Gigs.Include(g => g.Category).Include(g => g.Freelancer).ToListAsync();
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
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Gig>> PostGig(Gig gig)
		{
			_context.Gigs.Add(gig);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetGig", new { id = gig.Id }, gig);
		}

		// DELETE: api/Gigs/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Gig>> DeleteGig(int id)
		{
			var gig = await _context.Gigs.FindAsync(id);
			if (gig == null)
			{
				return NotFound();
			}

			_context.Gigs.Remove(gig);
			await _context.SaveChangesAsync();

			return gig;
		}

		private bool GigExists(int id)
		{
			return _context.Gigs.Any(e => e.Id == id);
		}
	}
}
