using ScaleStore.Core.DTOs.Product;
using ScaleStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ScaleStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get list of all products
        /// </summary>
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get a product by its ID
        /// </summary>
        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        /// <summary>
        /// Create a new product
        /// </summary>
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var product = await _productService.CreateProductAsync(dto);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Update an existing product by its ID
        /// </summary>
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var success = await _productService.UpdateProductAsync(id, dto);

            if (!success)
                return NotFound($"Product with ID {id} not found.");

            return NoContent();
        }

        /// <summary>
        /// Delete a product by its ID
        /// </summary>
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            
            if (!success)
                return NotFound($"Product with ID {id} not found.");

            return NoContent();
        }
    }
}
