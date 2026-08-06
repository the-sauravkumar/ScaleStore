using ScaleStore.Core.Entities;
using ScaleStore.Core.DTOs.Order;
using ScaleStore.Core.Mappings;
using ScaleStore.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ScaleStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ScaleStoreDbContext _context;

        public OrdersController(ScaleStoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            var response = orders
                .Select(o => o.ToResponseDto())
                .ToList();

            return Ok(response);
        }

        /// <summary>
        /// Get a order by its ID
        /// </summary>
        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null)
                return NotFound($"Order with ID {id} not found.");

            return Ok(order.ToResponseDto());
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var order = dto.ToEntity();
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            var response = order.ToResponseDto();

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, response);
        }

        /// <summary>
        /// Update an existing order
        /// </summary>
        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null)
                return NotFound($"Order with ID {id} not found.");

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Delete an order by its ID
        /// </summary>
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound($"Order with ID {id} not found.");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
