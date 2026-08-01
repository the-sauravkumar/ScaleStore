namespace ScaleStore.Core.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set;  }
        public string Sku { get; set;   }
        public decimal Price { get; set;  }
        public int StockQuantity { get; set; }
    }
}
