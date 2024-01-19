using Microsoft.EntityFrameworkCore;
using System.Text;
using WarehouseAccountingJewelryStoreService.Objects;

namespace WarehouseAccountingJewelryStoreService
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Supplirs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Encoding.Default.GetBytes("Host=localhost;Port=5433;Database=Warehouse;Username=qwert;Password=123");
            optionsBuilder.UseNpgsql(Encoding.UTF8.GetString(connectionString));
        }
    }
}
