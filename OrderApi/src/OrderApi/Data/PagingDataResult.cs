using System.Collections.Generic;
using OrderApi.Data.Entities;

namespace OrderApi.Data
{
    public class PagingDataResult
    {
        public IEnumerable<OrderEntity> Orders { get; set; }
        public int TotalRecords { get; set; }
    }
}
