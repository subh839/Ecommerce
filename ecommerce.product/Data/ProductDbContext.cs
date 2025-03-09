using Microsoft.EntityFrameworkCore;

namespace ecommerce.product.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<model.ProductModel>().HasData(new model.ProductModel { Id=1 , Name="Shirt" , Description="Allen",
            Price=300 , Quantity=29});
            modelBuilder.Entity<model.ProductModel>().HasData(new model.ProductModel
            {
                Id = 2,
                Name = "T-Shirt",
                Description = "Allen Solly",
                Price = 800,
                Quantity = 39
            });
            modelBuilder.Entity<model.ProductModel>().HasData(new model.ProductModel
            {
                Id = 3,
                Name = "Pant",
                Description = "Denim",
                Price = 100,
                Quantity = 11
            });

        }

        public DbSet<model.ProductModel> Products { get; set; }

    }
}
