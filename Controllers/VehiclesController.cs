using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController(VehicleRentalContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
            if (vehicle is null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Vehicle updatedVehicle)
        {
            var vehicleToUpdate = await _context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
            if (vehicleToUpdate is null)
            {
                return NotFound();
            }

            vehicleToUpdate.Model = updatedVehicle.Model;
            vehicleToUpdate.LicensePlate = updatedVehicle.LicensePlate;
            vehicleToUpdate.DailyRate = updatedVehicle.DailyRate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleToDelete = await _context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
            if (vehicleToDelete is null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicleToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}