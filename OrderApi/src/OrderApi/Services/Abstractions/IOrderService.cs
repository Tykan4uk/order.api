using System.Collections.Generic;
using System.Threading.Tasks;
using OrderApi.Models;
using OrderApi.Models.Responses;

namespace OrderApi.Services.Abstractions
{
    public interface IOrderService
    {
        Task<GetByPageResponse> GetByPageAsync(string userId, int page, int pageSize);
        Task<GetByIdResponse> GetByIdAsync(string userId, string orderId);
        Task<AddResponse> AddAsync(string userId, List<ProductModel> products);
    }
}
