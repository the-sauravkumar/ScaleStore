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

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            var response = products
                .Select(p => p.ToResponseDto())
                .ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task CreateProduct(CreateProductDto ProductDto)
        {
            var product = ProductDto.ToEntity();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var response = product.ToResponseDto();

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, response);
        }
    }
}
