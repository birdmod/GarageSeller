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
    public class CarsController : ControllerBase
    {
        private readonly IGarageSellerContext _context;

        public CarsController(IGarageSellerContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public IEnumerable<Api.Models.Car> GetCars()
        {
            return _context.Cars.Select(
                entity => new Api.Models.Car {
                    Model = entity.Model,
                    Price = entity.Price,
                    Year = entity.Year,
                    SerialNumber = entity.SerialNumber,
                    SoldDateUtc = entity.SoldDateUtc,
                    Comment = entity.Comment,
                    Motor = (Api.Models.Motor)entity.MotorID,
                    Transmission = (Api.Models.Transmission)entity.TransmissionID,
                    GarageId = entity.GarageID,
                    SellerId = entity.SellerID
                });
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar([FromRoute] int id)
        {
            var entityCar = await _context.Cars.FindAsync(id);

            if (entityCar == null)
                return NotFound();

            var responseCar = new Api.Models.Car
            {
                Id = entityCar.ID,
                Model = entityCar.Model,
                Price = entityCar.Price,
                Year = entityCar.Year,
                SerialNumber = entityCar.SerialNumber,
                SoldDateUtc = entityCar.SoldDateUtc,
                Comment = entityCar.Comment,
                Motor = (Api.Models.Motor)entityCar.MotorID,
                Transmission = (Api.Models.Transmission)entityCar.TransmissionID,
                GarageId = entityCar.GarageID,
                SellerId = entityCar.SellerID
            };

            return Ok(responseCar);
        }

        // PUT: api/Cars/5
        [HttpPut]
        public async Task<IActionResult> PutCar([FromBody] Api.Models.CarInfo carInfo)
        {
            // idempotency
            if (_context.Cars.Any(e => e.Model == carInfo.Model && e.Year == carInfo.Year && e.Price == carInfo.Price 
            && e.SerialNumber == carInfo.SerialNumber && e.SoldDateUtc == carInfo.SoldDateUtc && e.Comment == carInfo.Comment 
            && e.TransmissionID == (int)carInfo.Transmission && e.MotorID == (int)carInfo.Motor && e.GarageID == carInfo.GarageId 
            && e.SellerID == carInfo.SellerId))
                return NoContent();

            var created = new Car
            {
                Model = carInfo.Model,
                Price = carInfo.Price,
                Year = carInfo.Year,
                SerialNumber = carInfo.SerialNumber,
                SoldDateUtc = carInfo.SoldDateUtc,
                Comment = carInfo.Comment,
                MotorID = _context.MotorTypes.Find((int)carInfo.Motor).ID,
                TransmissionID = _context.TransmissionTypes.Find((int)carInfo.Transmission).ID,
                GarageID = carInfo.GarageId,
                SellerID = carInfo.SellerId

            };
            _context.Cars.Add(created);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = created.ID }, created);
        }

        // POST: api/Cars
        [HttpPost("{id}")]
        public async Task<IActionResult> PostCar([FromRoute] int id, [FromBody] Api.Models.CarInfo carInfo)
        {
            var toUpdate = await _context.Cars.FindAsync(id);
            if (toUpdate == null)
                return NotFound();

            toUpdate.Model = carInfo.Model;
            toUpdate.Price = carInfo.Price;
            toUpdate.Year = carInfo.Year;
            toUpdate.SerialNumber = carInfo.SerialNumber;
            toUpdate.SoldDateUtc = carInfo.SoldDateUtc;
            toUpdate.Comment = carInfo.Comment;
            toUpdate.MotorID = _context.MotorTypes.Find((int)carInfo.Motor).ID;
            toUpdate.TransmissionID = _context.TransmissionTypes.Find((int)carInfo.Transmission).ID;
            toUpdate.GarageID = carInfo.GarageId;
            toUpdate.SellerID = carInfo.SellerId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}