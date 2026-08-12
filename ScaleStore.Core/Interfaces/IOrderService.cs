using ScaleStore.Core.DTOs.Order;

namespace ScaleStore.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync(OrderQueryParameters query);
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
        Task<OrderCreatedResponseDto> CreateOrderAsync(CreateOrderDto dto);
        Task<bool> UpdateOrderAsync(int id, UpdateOrderDto dto);
        Task<bool> DeleteOrderAsync(int id);
    }
}
