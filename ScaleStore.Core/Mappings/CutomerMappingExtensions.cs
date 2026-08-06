using ScaleStore.Core.Entities;
using ScaleStore.Core.DTOs.Customer;

namespace ScaleStore.Core.Mappings
{
    public static class CustomerMappingExtensions
    {
        public static Customer ToEntity(this CreateCustomerDto dto)
        {
            return new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

        public static CustomerResponseDto ToResponseDto(this Customer dto)
        {
            return new CustomerResponseDto
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

        public static CustomerCreatedResponseDto ToCustomerCreatedDto(this Customer dto)
        {
            return new CustomerCreatedResponseDto
            {
                Id = dto.Id,
                Email = dto.Email,
            };
        }

        public static void UpdateFromDto(this Customer customer, UpdateCustomerDto dto)
        {
            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
        }
    }
}
