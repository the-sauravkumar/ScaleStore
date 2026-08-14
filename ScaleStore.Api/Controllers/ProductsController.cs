using ScaleStore.Core.DTOs.Product;
using ScaleStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;


namespace ScaleStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<CreateProductDto> _createProductValidator;
        private readonly IValidator<UpdateProductDto> _updateProductValidator;
        public ProductsController(
            IProductService productService,
            IValidator<CreateProductDto> createProductValidator,
            IValidator<UpdateProductDto> updateProductValidator)
        {
            _productService = productService;
            _createProductValidator = createProductValidator;
            _updateProductValidator = updateProductValidator;
        }

        /// <summary>
        /// Get list of all products
        /// </summary>
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParams)
        {
            var products = await _productService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        /// <summary>
        /// Get a product by its ID
        /// </summary>
        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }
        /// <summary>
        /// Create a new product
        /// </summary>
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {

            var validationResult = await _createProductValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }

            var product = await _productService.CreateProductAsync(dto);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Update an existing product by its ID
        /// </summary>
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var validationResult = await _updateProductValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }

            var result = await _productService.UpdateProductAsync(id, dto);
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
