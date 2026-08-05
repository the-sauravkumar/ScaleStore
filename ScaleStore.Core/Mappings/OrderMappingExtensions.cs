using ScaleStore.Core.Entities;
using ScaleStore.Core.DTOs.Order;

namespace ScaleStore.Core.Mappings
{
    public static class OrderMappingExtensions
    {
        // For POST request DTO
        public static Order ToEntity(this CreateOrderDto dto)
        {
            return new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount
            };
        }

        // For GET response DTO
        public static OrderResponseDto ToResponseDto(this Order dto)
        {
            return new OrderResponseDto
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount
            };
        }

        // For POST response DTO
        public static OrderCreatedResponseDto ToOrderCreatedDto(this Order dto)
        {
            return new OrderCreatedResponseDto
            {
                Id = dto.Id,
                OrderDate = dto.OrderDate,
            };
        }

        public static void UpdateFromDto(this Order order, UpdateOrderDto dto)
        {
            order.CustomerId = dto.CustomerId;
            order.OrderDate = dto.OrderDate;
            order.TotalAmount = dto.TotalAmount;
        }
    }
}
