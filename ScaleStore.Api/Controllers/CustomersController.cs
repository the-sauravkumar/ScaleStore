using ScaleStore.Core.Interfaces;
using ScaleStore.Core.DTOs.Customer;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace ScaleStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get list of all customers
        /// </summary>
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] CustomerQueryParameters queryParams)
        {
            var customers = await _customerService.GetAllCustomersAsync(queryParams);
            return Ok(customers);
        }

        /// <summary>
        /// Get a customer by ID
        /// </summary>
        [HttpGet("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            return Ok(customer);
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            var customer = await _customerService.CreateCustomerAsync(dto);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Update an existing customer by id
        /// </summary>
        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto dto)
        {
            await _customerService.UpdateCustomerAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Delete a customer by id
        /// </summary>
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.DeleteCustomerAsync(id);

            if (!customer)
                return NotFound($"Customer with ID {id} not found.");

            return NoContent();
        }
    }
}
