using ScaleStore.Core.Entities;
using ScaleStore.Core.DTOs.Product;
using ScaleStore.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScaleStore.Core.Mappings;

namespace ScaleStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ScaleStoreDbContext _context;

        public ProductsController(ScaleStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            var response = products
                .Select(p => p.ToResponseDto())
                .ToList();

            return Ok(response);
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product.ToResponseDto());
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var product = dto.ToEntity();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var response = product.ToResponseDto();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            product.UpdateFromDto(dto);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();


        }
    }
}
