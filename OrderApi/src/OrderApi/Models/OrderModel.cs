using System;

namespace OrderApi.Models
{
    public class OrderModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
