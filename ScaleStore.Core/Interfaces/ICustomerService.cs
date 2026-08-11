using ScaleStore.Core.DTOs.Customer;

namespace ScaleStore.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync();
        Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);
        Task<CustomerCreatedResponseDto> CreateCustomerAsync(CreateCustomerDto dto);
        Task<bool> UpdateCustomerAsync(int id, UpdateCustomerDto dto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
