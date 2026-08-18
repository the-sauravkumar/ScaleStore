namespace ScaleStore.Core.Entities
{
    public class OrderItem
    {
        // Foreign Key to the Order
        public int OrderId { get; set; }
        public Order Order { get; set; } = null;

        // Foreign Key to the Product
        public int ProductId { get; set; }
        public Product Product { get; set; } = null;

        // The snapshot of data at the time of purchase
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
