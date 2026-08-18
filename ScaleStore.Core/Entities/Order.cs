using System;

namespace ScaleStore.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Foreign Key to the Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null;

        // Navigation property: An order can have multiple line items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
