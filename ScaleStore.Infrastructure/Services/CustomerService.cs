using ScaleStore.Core.Mappings;
using ScaleStore.Core.Interfaces;
using ScaleStore.Infrastructure.Data;
using ScaleStore.Core.DTOs.Customer;
using Microsoft.EntityFrameworkCore;


namespace ScaleStore.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ScaleStoreDbContext _context;

        public CustomerService(ScaleStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers.Select (c => c.ToResponseDto());
        }

        public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer?.ToResponseDto();
        }

        public async Task<CustomerCreatedResponseDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = dto.ToEntity();

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer.ToCustomerCreatedDto();
        }

        public async Task<bool> UpdateCustomerAsync(int id, UpdateCustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return false;

            customer.UpdateFromDto(dto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
