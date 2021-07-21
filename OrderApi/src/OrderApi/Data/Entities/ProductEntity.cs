using OrderApi.Common.Enums;

namespace OrderApi.Data.Entities
{
    public class ProductEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductTypeEnum Type { get; set; }

        public string OrderId { get; set; }
        public OrderEntity Order { get; set; }
    }
}
