using System;

namespace ScaleStore.Core.DTOs.Order
{
    public class UpdateOrderDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
