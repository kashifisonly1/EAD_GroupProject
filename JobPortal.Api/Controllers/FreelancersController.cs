using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.BusinessModels;
using JobPortal.BusinessModels.Freelancers;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancersController : ControllerBase
    {
        private readonly JobPortalContext _context;

        public FreelancersController(JobPortalContext context)
        {
            _context = context;
        }

        // GET: api/Freelancers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Freelancer>>> GetFreelancers()
        {
            return await _context.Freelancers.ToListAsync();
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

            return freelancer;
        }

        // PUT: api/Freelancers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Freelancer>> PostFreelancer(Freelancer freelancer)
        {
            _context.Freelancers.Add(freelancer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFreelancer", new { id = freelancer.Id }, freelancer);
        }

        // DELETE: api/Freelancers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Freelancer>> DeleteFreelancer(int id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer == null)
            {
                return NotFound();
            }

            _context.Freelancers.Remove(freelancer);
            await _context.SaveChangesAsync();

            return freelancer;
        }

        private bool FreelancerExists(int id)
        {
            return _context.Freelancers.Any(e => e.Id == id);
        }
    }
}
