using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController(VehicleRentalContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rentals = await _context.Rentals.ToListAsync();
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(rental => rental.Id == id);
            if (rental is null)
            {
                return NotFound();
            }
            return Ok(rental);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = rental.Id }, rental);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Rental updatedRental)
        {
            var rentalToUpdate = await _context.Rentals.FirstOrDefaultAsync(rental => rental.Id == id);
            if (rentalToUpdate is null)
            {
                return NotFound();
            }

            rentalToUpdate.CustomerId = updatedRental.CustomerId;
            rentalToUpdate.VehicleId = updatedRental.VehicleId;
            rentalToUpdate.RentalDate = updatedRental.RentalDate;
            rentalToUpdate.ReturnDate = updatedRental.ReturnDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rentalToDelete = await _context.Rentals.FirstOrDefaultAsync(rental => rental.Id == id);
            if (rentalToDelete is null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rentalToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
