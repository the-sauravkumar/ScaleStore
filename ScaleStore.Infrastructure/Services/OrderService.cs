using ScaleStore.Core.Interfaces;
using ScaleStore.Core.Entities;
using ScaleStore.Core.Mappings;
using ScaleStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ScaleStore.Core.DTOs.Order;

namespace ScaleStore.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ScaleStoreDbContext _context;

        public OrderService(ScaleStoreDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order?.ToResponseDto();
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync(OrderQueryParameters queryParameters)
        {
            var query = _context.Orders.AsQueryable();

            if (queryParameters.CustomerId.HasValue)
            {
                query = query.Where(o => o.CustomerId == queryParameters.CustomerId.Value);
            }

           if (queryParameters.MinAmount.HasValue)
            {
                query = query.Where(o => o.TotalAmount >= queryParameters.MinAmount.Value);
            }

           if (queryParameters.MaxAmount.HasValue)
            {
                query = query.Where(o => o.TotalAmount <= queryParameters.MaxAmount.Value);
            }

            query = queryParameters.SortBy?.ToLower() switch
            {
                "date_asc" => query.OrderBy(o => o.OrderDate),
                "date_desc" => query.OrderByDescending(o => o.OrderDate),
                _ => query.OrderBy(o => o.Id)
            };

            var skipAmount = (queryParameters.PageNumber - 1) * queryParameters.PageSize;

            var orders = await query
                .Skip(skipAmount)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return orders.Select(o => o.ToResponseDto());
        }

        public async Task<OrderCreatedResponseDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = dto.ToEntity();
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return order.ToOrderCreatedDto();
        }

        public async Task<bool> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return false;

            order.UpdateFromDto(dto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
