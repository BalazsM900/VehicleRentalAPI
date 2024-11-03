using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(VehicleRentalContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            if (customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer UpdatedCustomer)
        {
            var customerToUpdate = await _context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            if (customerToUpdate is null)
            {
                return NotFound();
            }

            customerToUpdate.Name = UpdatedCustomer.Name;
            customerToUpdate.Email = UpdatedCustomer.Email;
            customerToUpdate.PhoneNumber = UpdatedCustomer.PhoneNumber;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerToDelete = await _context.Customers.FirstOrDefaultAsync(costumer => costumer.Id == id);
            if (customerToDelete is null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}