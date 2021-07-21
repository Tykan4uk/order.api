using System;
using System.Collections.Generic;

namespace OrderApi.Data.Entities
{
    public class OrderEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
