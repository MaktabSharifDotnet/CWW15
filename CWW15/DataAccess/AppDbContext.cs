using CWW15.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWW15.DataAccess
{
    public class AppDbContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-M2BLLND\\SQLEXPRESS;Database=EShopDb;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Digital Goods" },
                new Category { Id = 2, Name = "Home Appliances" }
            );

          
            modelBuilder.Entity<Product>().HasData(
                
                new Product { Id = 1, Name = "Laptop", Price = 1200.00m, Color = "Silver", Brand = "Apple", Stock = 15, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Price = 800.00m, Color = "Black", Brand = "Samsung", Stock = 50, CategoryId = 1 },
                new Product { Id = 3, Name = "Headphones", Price = 150.50m, Color = "White", Brand = "Sony", Stock = 100, CategoryId = 1 },
                new Product { Id = 4, Name = "Smartwatch", Price = 250.00m, Color = "Gray", Brand = "Garmin", Stock = 30, CategoryId = 1 },
                new Product { Id = 5, Name = "Gaming Console", Price = 500.00m, Color = "Black", Brand = "Microsoft", Stock = 25, CategoryId = 1 },

                new Product { Id = 6, Name = "Refrigerator", Price = 1500.00m, Color = "Stainless Steel", Brand = "LG", Stock = 10, CategoryId = 2 },
                new Product { Id = 7, Name = "Washing Machine", Price = 900.00m, Color = "White", Brand = "Bosch", Stock = 20, CategoryId = 2 },
                new Product { Id = 8, Name = "Microwave Oven", Price = 200.00m, Color = "Black", Brand = "Panasonic", Stock = 40, CategoryId = 2 },
                new Product { Id = 9, Name = "Vacuum Cleaner", Price = 300.00m, Color = "Red", Brand = "Dyson", Stock = 35, CategoryId = 2 },
                new Product { Id = 10, Name = "Coffee Maker", Price = 120.99m, Color = "Black", Brand = "Philips", Stock = 60, CategoryId = 2 }
            );
        }
    }
}
