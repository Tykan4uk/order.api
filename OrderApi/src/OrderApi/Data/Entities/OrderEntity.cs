using System;
using OrderApi.Common.Enums;

namespace OrderApi.Data.Entities
{
    public class OrderEntity
    {
        public string OrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductTypeEnum Type { get; set; }
    }
}
