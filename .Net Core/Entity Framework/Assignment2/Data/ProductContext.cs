using Assignment2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> option) : base(option) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasMany<Product>(p => p.Products)
                        .WithOne(c => c.Category)
                        .HasForeignKey(s => s.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Book"
                });
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, CategoryId = 1, ProductName = "Hamlet", Manufacture = "HN" },
                new Product { ProductId = 2, CategoryId = 1, ProductName = "ABC", Manufacture = "HN" },
                new Product { ProductId = 3, CategoryId = 1, ProductName = "EFG", Manufacture = "HN" }
            );
        }
    }
}