using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Data.Entities;
using OrderApi.DataProviders.Abstractions;

namespace OrderApi.DataProviders
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrdersDbContext _ordersDbContext;

        public OrderProvider(IDbContextFactory<OrdersDbContext> dbContextFactory)
        {
            _ordersDbContext = dbContextFactory.CreateDbContext();
        }

        public async Task<PagingDataResult> GetByPageAsync(string userId, int page, int pageSize)
        {
            var totalRecords = await _ordersDbContext.Orders.Where(w => w.UserId == userId).CountAsync();
            var pageData = await _ordersDbContext.Orders
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
            var result = await _ordersDbContext.Orders.AddAsync(
                new OrderEntity()
                {
                    Id = id,
                    UserId = userId,
                    CreateDate = createDate,
                    Products = products
                });
            await _ordersDbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
