namespace ScaleStore.Core.DTOs.Order
{
    public class OrderQueryParameters
    {
        public int? CustomerId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }

        public string? SortBy { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
