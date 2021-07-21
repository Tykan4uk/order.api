using System.Collections.Generic;

namespace OrderApi.Models.Responses
{
    public class GetByPageResponse
    {
        public IEnumerable<OrderModel> Orders { get; set; }
        public int TotalRecords { get; set; }
    }
}
