using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.BusinessModels;
using JobPortal.BusinessModels.Orders;

namespace JobPortal.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderDeliveriesController : ControllerBase
	{
		private readonly JobPortalContext _context;

		public OrderDeliveriesController(JobPortalContext context)
		{
			_context = context;
		}

		// GET: api/OrderDeliveries
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderDelivery>>> GetOrdersDelivery()
		{
			return await _context.OrdersDelivery.Include(o => o.Order).ToListAsync();
		}

		// GET: api/OrderDeliveries/5
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDelivery>> GetOrderDelivery(int id)
		{
			var orderDelivery = await _context.OrdersDelivery.FindAsync(id);

			await _context.Entry(orderDelivery).Reference(o => o.Order).LoadAsync();

			if (orderDelivery == null)
			{
				return NotFound();
			}

			return orderDelivery;
		}

		// PUT: api/OrderDeliveries/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrderDelivery(int id, OrderDelivery orderDelivery)
		{
			if (id != orderDelivery.Id)
			{
				return BadRequest();
			}

			_context.Entry(orderDelivery).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderDeliveryExists(id))
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

		// POST: api/OrderDeliveries
		[HttpPost]
		public async Task<ActionResult<OrderDelivery>> PostOrderDelivery(OrderDelivery orderDelivery)
		{
			_context.OrdersDelivery.Add(orderDelivery);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetOrderDelivery", new { id = orderDelivery.Id }, orderDelivery);
		}

		// DELETE: api/OrderDeliveries/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<OrderDelivery>> DeleteOrderDelivery(int id)
		{
			var orderDelivery = await _context.OrdersDelivery.FindAsync(id);
			if (orderDelivery == null)
			{
				return NotFound();
			}

			_context.OrdersDelivery.Remove(orderDelivery);
			await _context.SaveChangesAsync();

			return orderDelivery;
		}

		private bool OrderDeliveryExists(int id)
		{
			return _context.OrdersDelivery.Any(e => e.Id == id);
		}
	}
}
