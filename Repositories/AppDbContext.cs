using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public  class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //var product = new Product { ProductFeature = new ProductFeature() };
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductFeature> ProductFeature { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // best practice için burada ürün eklememek daha iyi
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature() {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 200,
                ProductId = 1},
                new ProductFeature()
                {
                    Id = 2,
                    Color = "Mavi",
                    Height = 300,
                    Width = 500,
                    ProductId = 2
                });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
