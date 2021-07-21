using System.Collections.Generic;
using System.Threading.Tasks;
using OrderApi.Data;
using OrderApi.Data.Entities;

namespace OrderApi.DataProviders.Abstractions
{
    public interface IOrderProvider
    {
        Task<PagingDataResult> GetByPageAsync(string userId, int page, int pageSize);
        Task<List<ProductEntity>> GetByIdAsync(string userId, string orderId);
        Task<OrderEntity> AddAsync(string userId, List<ProductEntity> products);
    }
}
