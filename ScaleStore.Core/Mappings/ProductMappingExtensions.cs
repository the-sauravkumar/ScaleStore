using ScaleStore.Core.DTOs.Product;
using ScaleStore.Core.Entities;
using System.Runtime.CompilerServices;

namespace ScaleStore.Core.Mappings
{
    public static class ProductMappingExtensions
    {
        public static Product ToEntity (this CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Sku = dto.Sku,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };
        }

        // Full GET response DTO
        public static ProductResponseDto ToResponseDto(this Product entity)
        {
            return new ProductResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Sku = entity.Sku,
                Price = entity.Price,
                StockQuantity = entity.StockQuantity
            };
        }

        // For POST response DTO
        public static ProductCreatedResponseDto ToCreatedResponseDto(this Product entity)
        {
            return new ProductCreatedResponseDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static void UpdateFromDto(this Product product, UpdateProductDto dto)
        {
            product.Name = dto.Name;
            product.Sku = dto.Sku;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
        }
    }
}
