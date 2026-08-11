using ScaleStore.Core.Interfaces;
using ScaleStore.Core.Mappings;
using ScaleStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ScaleStore.Core.DTOs.Product;

namespace ScaleStore.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ScaleStoreDbContext _context;

        public ProductService(ScaleStoreDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product?.ToResponseDto();
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products.Select(p => p.ToResponseDto());
        }

        public async Task<ProductCreatedResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = dto.ToEntity();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.ToCreatedResponseDto();
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return false;

            product.UpdateFromDto(dto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
