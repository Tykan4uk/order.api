using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderApi.Common.Exceptions;
using OrderApi.Data;
using OrderApi.Data.Entities;
using OrderApi.DataProviders.Abstractions;
using OrderApi.Services.Abstractions;

namespace OrderApi.DataProviders
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrdersDbContext _ordersDbContext;
        private readonly ILogger<OrderProvider> _logger;

        public OrderProvider(
            IDbContextWrapper<OrdersDbContext> dbContextWrapper,
            ILogger<OrderProvider> logger)
        {
            _ordersDbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PagingDataResult> GetByPageAsync(string userId, int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                _logger.LogError("(OrderProvider/GetByPageAsync)Page or page size error!");
                throw new BusinessException("Page or page size error!");
            }

            var totalRecords = await _ordersDbContext.Orders.Where(w => w.UserId == userId).CountAsync();
            var pageData = await _ordersDbContext.Orders
                .Where(w => w.UserId == userId)
                .OrderBy(o => o.CreateDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingDataResult() { Orders = pageData, TotalRecords = totalRecords };
        }

        public async Task<List<ProductEntity>> GetByIdAsync(string userId, string orderId)
        {
            var checkOrder = await _ordersDbContext.Orders.FirstOrDefaultAsync(f => f.Id == orderId && f.UserId == userId);
            if (checkOrder != null)
            {
                return await _ordersDbContext.Products.Where(w => w.OrderId == orderId).ToListAsync();
            }

            return null;
        }

        public async Task<OrderEntity> AddAsync(string userId, List<ProductEntity> products)
        {
            var createDate = DateTime.Now;
            var id = Guid.NewGuid().ToString();
            var order = new OrderEntity()
            {
                Id = id,
                UserId = userId,
                CreateDate = createDate,
                Products = products
            };
            var result = await _ordersDbContext.Orders.AddAsync(order);
            await _ordersDbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
