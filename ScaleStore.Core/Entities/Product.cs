namespace ScaleStore.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Navigation property: A product can be in many different order items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
