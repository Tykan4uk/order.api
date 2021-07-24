using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrderApi.Data;
using OrderApi.Services;
using OrderApi.Services.Abstractions;

namespace OrderApi.UnitTests.Mocks
{
    public class MockService : BaseDataService<OrdersDbContext>
    {
        public MockService(
            IDbContextWrapper<OrdersDbContext> dbContextWrapper,
            ILogger<MockService> logger)
            : base(dbContextWrapper, logger)
        {
        }

        public async Task RunWithException()
        {
            await ExecuteSafe<bool>(() =>
            {
                throw new Exception();
            });
        }

        public async Task RunWithoutException()
        {
            await ExecuteSafe<bool>(() =>
            {
                return Task.FromResult(true);
            });
        }
    }
}
