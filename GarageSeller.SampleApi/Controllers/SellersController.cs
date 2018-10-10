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
    public class SellersController : ControllerBase
    {
        private readonly IGarageSellerContext _context;

        public SellersController(IGarageSellerContext context)
        {
            _context = context;
        }

        // GET: api/Sellers
        [HttpGet]
        public IEnumerable<Api.Models.Seller> GetSellers()
        {
            return _context.Sellers.Select(
                entity => new Api.Models.Seller {
                    Id = entity.ID,
                    Address = entity.Address,
                    Email = entity.Email,
                    Phone = entity.Phone,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName
                } );
        }

        // GET: api/Sellers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeller([FromRoute] int id)
        {
            var entitySeller = await _context.Sellers.FindAsync(id);

            if (entitySeller == null)
                return NotFound();

            var responseSeller = new Api.Models.Seller
            {
                Id = entitySeller.ID,
                Address = entitySeller.Address,
                Email = entitySeller.Email,
                Phone = entitySeller.Phone,
                FirstName = entitySeller.FirstName,
                LastName = entitySeller.LastName
            };

            return Ok(responseSeller);
        }

        // PUT: api/Sellers/5
        [HttpPut]
        public async Task<IActionResult> PutSeller([FromBody] Api.Models.SellerInfo seller)
        {
            // idempotency
            if (_context.Sellers.Any(e => e.Address == seller.Address && e.Email == seller.Email
            && e.Phone == seller.Phone && e.LastName == seller.LastName && e.FirstName == seller.FirstName))
                return NoContent();

            var created = new Seller
            {
                Address = seller.Address,
                Email = seller.Email,
                Phone = seller.Phone,
                FirstName = seller.FirstName,
                LastName = seller.LastName
            };
            _context.Sellers.Add(created);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeller", new { id = created.ID }, created);
        }

        // POST: api/Sellers
        [HttpPost("{id}")]
        public async Task<IActionResult> PostSeller([FromRoute] int id, [FromBody] Api.Models.ContactInfo contactInfo)
        {
            var toUpdate = await _context.Sellers.FindAsync(id);
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