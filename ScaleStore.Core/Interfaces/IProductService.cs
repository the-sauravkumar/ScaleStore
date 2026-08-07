using ScaleStore.Core.DTOs;
using ScaleStore.Core.DTOs.Product;
using System.Threading.Tasks;

namespace ScaleStore.Core.Interfaces
{
    public interface IProductService
    {
        // Promises to return a list of product
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        
        // Promises to return a single product (nullable)
        Task<ProductResponseDto?> GetProductByIdAsync(int id);
        Task <ProductCreatedResponseDto> CreateProductAsync(CreateProductDto dto);

        // Promises to return true if update succeeded, false if not found
        Task<bool> UpdateProductAsync(int id, UpdateProductDto dto);

        // true if delete, false if not found
        Task<bool> DeleteProductAsync(int id);
    }
}
