using System.Collections.Generic;

namespace OrderApi.Models.Requests
{
    public class AddRequest
    {
        public string UserId { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
