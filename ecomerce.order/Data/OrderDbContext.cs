using Microsoft.EntityFrameworkCore;

namespace ecommerce.order.Data
{
    public class OrderDbContext: DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

      

        public DbSet<model.OrderModel> Orders { get; set; }
    }
}
