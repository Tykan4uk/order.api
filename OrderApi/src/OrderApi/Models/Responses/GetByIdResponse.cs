using System.Collections.Generic;

namespace OrderApi.Models.Responses
{
    public class GetByIdResponse
    {
        public List<ProductModel> Products { get; set; }
    }
}
