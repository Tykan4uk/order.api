using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Data.Entities;
using OrderApi.DataProviders.Abstractions;
using OrderApi.Models;
using OrderApi.Models.Responses;
using OrderApi.Services.Abstractions;

namespace OrderApi.Services
{
    public class OrderService : BaseDataService, IOrderService
    {
        private readonly IOrderProvider _orderProvider;
        private readonly IMapper _mapper;

        public OrderService(
            IDbContextFactory<OrdersDbContext> factory,
            IOrderProvider orderProvider,
            IMapper mapper)
            : base(factory)
        {
            _orderProvider = orderProvider;
            _mapper = mapper;
        }

        public async Task<GetByPageResponse> GetByPageAsync(string userId, int page, int pageSize)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _orderProvider.GetByPageAsync(userId, page, pageSize);

                return _mapper.Map<GetByPageResponse>(result);
            });
        }

        public async Task<GetByIdResponse> GetByIdAsync(string userId, string orderId)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _orderProvider.GetByIdAsync(userId, orderId);

                var products = _mapper.Map<List<ProductModel>>(result);

                return new GetByIdResponse() { Products = products };
            });
        }

        public async Task<AddResponse> AddAsync(string userId, List<ProductModel> products)
        {
            return await ExecuteSafe(async () =>
            {
                var productsEntity = _mapper.Map<List<ProductEntity>>(products);
                var result = await _orderProvider.AddAsync(userId, productsEntity);

                var order = _mapper.Map<OrderModel>(result);

                return new AddResponse() { Order = order };
            });
        }
    }
}
