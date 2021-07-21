using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Entities;

namespace OrderApi.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrderEntity> Orders { get; set; } = null!;
    }
}
