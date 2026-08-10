using ScaleStore.Core.DTOs.Order;
using Microsoft.AspNetCore.Mvc;
using ScaleStore.Core.Interfaces;

namespace ScaleStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get list of all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        /// <summary>
        /// Get a order by its ID
        /// </summary>
        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if(order == null)
                return NotFound($"Order with ID {id} not found.");

            return Ok(order);
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var order = await _orderService.CreateOrderAsync(dto);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Update an existing order
        /// </summary>
        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto dto)
        {
            var order = await _orderService.UpdateOrderAsync(id, dto);

            if(order == false)
                return NotFound($"Order with ID {id} not found.");

            return NoContent();
        }

        /// <summary>
        /// Delete an order by its ID
        /// </summary>
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.DeleteOrderAsync(id);

            if (order == false)
                return NotFound($"Order with ID {id} not found.");

            return NoContent();
        }
    }
}
