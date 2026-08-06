using ScaleStore.Core.Entities;
using ScaleStore.Core.DTOs.Customer;
using ScaleStore.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScaleStore.Core.Mappings;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ScaleStore.Api.Controllers
{
    public class CustomersController : ControllerBase
    {
        private readonly ScaleStoreDbContext _context;

        public CustomersController(ScaleStoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of all customers
        /// </summary>
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            var response = customers
                .Select(c => c.ToResponseDto())
                .ToList();

            return Ok(response);
        }

        /// <summary>
        /// Get a customer by ID
        /// </summary>
        [HttpGet("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            return Ok(customer.ToResponseDto());
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            var customer = dto.ToEntity();

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var response = customer.ToResponseDto();

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, response);
        }

        /// <summary>
        /// Update an existing customer by id
        /// </summary>
        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            customer.UpdateFromDto(dto);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a customer by id
        /// </summary>
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
