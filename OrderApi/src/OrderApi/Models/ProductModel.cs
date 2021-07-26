using OrderApi.Common.Enums;

namespace OrderApi.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductTypeEnum Type { get; set; }
        public int Count { get; set; }
    }
}
