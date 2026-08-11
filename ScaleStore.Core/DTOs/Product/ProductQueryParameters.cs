namespace ScaleStore.Core.DTOs.Product
{
    public class ProductQueryParameters
    {
        // (Text based)
        public string? SearchTerm { get; set; }

        //Filtering (Exact value or ranges)
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        // Sorting
        public string? SortBy { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
