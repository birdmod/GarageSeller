using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarageSeller.Context;
using GarageSeller.Models;
using GarageSeller.Context.Interfaces;

namespace GarageSeller.SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaragesController : ControllerBase
    {
        private readonly IGarageSellerContext _context;

        public GaragesController(IGarageSellerContext context)
        {
            _context = context;
        }

        // GET: api/Garages
        [HttpGet]
        public IEnumerable<Api.Models.Garage> GetGarages()
        {
            return _context.Garages.Select(
                entityGarage => new Api.Models.Garage {
                    Id = entityGarage.ID,
                    Address = entityGarage.Address,
                    Phone = entityGarage.Phone,
                    Email = entityGarage.Email,
                    _GarageName = entityGarage.GarageName
                });
        }

        // GET: api/Garages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGarage([FromRoute] int id)
        {
            var entityGarage = await _context.Garages.FindAsync(id);

            if (entityGarage == null)
                return NotFound();

            var responseGarage = new Api.Models.Garage {
                Id = entityGarage.ID,
                Address = entityGarage.Address,
                Email = entityGarage.Email,
                Phone = entityGarage.Phone,
                _GarageName = entityGarage.GarageName
            };

            return Ok(responseGarage);
        }

        // PUT: api/Garages
        [HttpPut()]
        public async Task<IActionResult> PutGarage([FromBody] Api.Models.GarageInfo garageInfo)
        {
            // idempotency
            if (_context.Garages.Any(e => e.Address == garageInfo.Address && e.Email == garageInfo.Email 
            && e.Phone == garageInfo.Phone && e.GarageName == garageInfo._GarageName))
                return NoContent();

            var created = new Garage
            {
                Address = garageInfo.Address,
                Email = garageInfo.Email,
                Phone = garageInfo.Phone,
                GarageName = garageInfo._GarageName // last time I use codegen out of the box I swear it dear god who is in heaven
            };
            
            _context.Garages.Add(created);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGarage", new { id = created.ID }, created);
        }

        // POST: api/Garages
        [HttpPost("{id}")]
        public async Task<IActionResult> PostGarage([FromRoute] int id, [FromBody] Api.Models.ContactInfo contactInfo)
        {
            var toUpdate = await _context.Garages.FindAsync(id);
            if (toUpdate == null)
                return NotFound();

            toUpdate.Address = contactInfo.Address;
            toUpdate.Email = contactInfo.Email;
            toUpdate.Phone = contactInfo.Phone;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}