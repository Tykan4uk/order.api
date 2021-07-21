using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;

namespace OrderApi.Services
{
    public class BaseDataService
    {
        private readonly OrdersDbContext _ordersDbContext;

        public BaseDataService(IDbContextFactory<OrdersDbContext> dbContextFactory)
        {
            _ordersDbContext = dbContextFactory.CreateDbContext();
        }

        protected async Task<T> ExecuteSafe<T>(Func<Task<T>> action)
        {
            using (var transaction = _ordersDbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = await action();
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
