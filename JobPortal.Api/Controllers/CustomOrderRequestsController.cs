using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.BusinessModels;
using JobPortal.BusinessModels.Client;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomOrderRequestsController : ControllerBase
    {
        private readonly JobPortalContext _context;

        public CustomOrderRequestsController(JobPortalContext context)
        {
            _context = context;
        }

        // GET: api/CustomOrderRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomOrderRequest>>> GetCustomOrderRequests()
        {
            return await _context.CustomOrderRequests.ToListAsync();
        }

        // GET: api/CustomOrderRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomOrderRequest>> GetCustomOrderRequest(int id)
        {
            var customOrderRequest = await _context.CustomOrderRequests.FindAsync(id);

            if (customOrderRequest == null)
            {
                return NotFound();
            }

            return customOrderRequest;
        }

        // PUT: api/CustomOrderRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomOrderRequest(int id, CustomOrderRequest customOrderRequest)
        {
            if (id != customOrderRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(customOrderRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomOrderRequestExists(id))
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

        // POST: api/CustomOrderRequests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomOrderRequest>> PostCustomOrderRequest(CustomOrderRequest customOrderRequest)
        {
            _context.CustomOrderRequests.Add(customOrderRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomOrderRequest", new { id = customOrderRequest.Id }, customOrderRequest);
        }

        // DELETE: api/CustomOrderRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomOrderRequest>> DeleteCustomOrderRequest(int id)
        {
            var customOrderRequest = await _context.CustomOrderRequests.FindAsync(id);
            if (customOrderRequest == null)
            {
                return NotFound();
            }

            _context.CustomOrderRequests.Remove(customOrderRequest);
            await _context.SaveChangesAsync();

            return customOrderRequest;
        }

        private bool CustomOrderRequestExists(int id)
        {
            return _context.CustomOrderRequests.Any(e => e.Id == id);
        }
    }
}
