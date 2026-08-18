namespace ScaleStore.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation property: A customer can have multiple orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
