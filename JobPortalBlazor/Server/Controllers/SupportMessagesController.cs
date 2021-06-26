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
	public class SupportMessagesController : ControllerBase
	{
		private readonly JobPortalDBContext _context;

		public SupportMessagesController(JobPortalDBContext context)
		{
			_context = context;
		}

		// GET: api/SupportMessages
		[HttpGet]
		public async Task<ActionResult<IEnumerable<SupportMessage>>> GetSupportMessages()
		{
			return await _context.SupportMessages
				.Include(s => s.User)
				.ToListAsync();
		}

		// GET: api/SupportMessages/5
		[HttpGet("{id}")]
		public async Task<ActionResult<SupportMessage>> GetSupportMessage(int id)
		{
			var supportMessage = await _context.SupportMessages.FindAsync(id);

			if (supportMessage == null)
			{
				return NotFound();
			}

			return supportMessage;
		}

		// PUT: api/SupportMessages/5
		[HttpPut("{id}")]
		public async Task<IActionResult> MarkAsResponded(int id)
		{

			var message = _context.SupportMessages.Find(id);
			message.IsResponded = true;

			_context.Entry(message).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!SupportMessageExists(id))
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

		// POST: api/SupportMessages
		[HttpPost]
		public async Task<ActionResult<SupportMessage>> PostSupportMessage(SupportMessage supportMessage)
		{
			_context.SupportMessages.Add(supportMessage);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetSupportMessage", new { id = supportMessage.Id }, supportMessage);
		}

		// DELETE: api/SupportMessages/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSupportMessage(int id)
		{
			var supportMessage = await _context.SupportMessages.FindAsync(id);
			if (supportMessage == null)
			{
				return NotFound();
			}

			_context.SupportMessages.Remove(supportMessage);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool SupportMessageExists(int id)
		{
			return _context.SupportMessages.Any(e => e.Id == id);
		}
		[HttpGet]
		[Route("/api/reset_all")]
		public async Task<IActionResult> DeleteDatabase()
		{
			var x = await _context.OrderDeliveries.ToListAsync();
			_context.OrderDeliveries.RemoveRange(await _context.OrderDeliveries.ToListAsync());
			_context.Orders.RemoveRange(await _context.Orders.ToListAsync());
			_context.CustomOrderRequests.RemoveRange(await _context.CustomOrderRequests.ToListAsync());
			_context.Gigs.RemoveRange(await _context.Gigs.ToListAsync());
			_context.Skills.RemoveRange(await _context.Skills.ToListAsync());
			_context.SupportMessages.RemoveRange(await _context.SupportMessages.ToListAsync());
			_context.Catogories.RemoveRange(await _context.Catogories.ToListAsync());
			_context.Freelancers.RemoveRange(await _context.Freelancers.ToListAsync());
			await _context.SaveChangesAsync();
			return Ok("everything deleted");
		}
	}
}
