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

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(ProductQueryParameters queryParams)
        {
            // Start query, don't execute it yet
            var query = _context.Products.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(queryParams.SearchTerm) ||
                                        p.Sku.Contains(queryParams.SearchTerm));
            }

            // Filter by price range (if provided)
            if (queryParams.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= queryParams.MinPrice.Value); 
            }

            if (queryParams.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= queryParams.MaxPrice.Value);
            }

            // Sort
            query = queryParams.SortBy?.ToLower() switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name_desc" => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Name) // Defaults to Name
            };

            // Pagination: Skip previous pages, and take the current page size
            var skipAmount = (queryParams.PageNumber - 1) * queryParams.PageSize;

            var products = await query
                .Skip(skipAmount)
                .Take(queryParams.PageSize)
                .ToListAsync();

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
