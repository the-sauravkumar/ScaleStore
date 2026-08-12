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

        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync(CustomerQueryParameters queryParams)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                query = query.Where(c => c.FirstName.Contains(queryParams.SearchTerm) ||
                                        c.LastName.Contains(queryParams.SearchTerm) ||
                                         c.Email.Contains(queryParams.SearchTerm));
            }

            query = queryParams.SortBy?.ToLower() switch
            {
                "first_name" => query.OrderBy(c => c.FirstName),
                "last_name" => query.OrderBy(c => c.LastName),
                "email_desc" => query.OrderByDescending(c => c.Email),
                "email_asc" => query.OrderBy(c => c.Email),
                _ => query.OrderBy(c => c.Id),
            };

            var skipAmount = (queryParams.PageNumber - 1) * queryParams.PageSize;


            var customers = await query
                .Skip(skipAmount)
                .Take(queryParams.PageSize)
                .ToListAsync();

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
