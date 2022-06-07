using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        // Fluent API
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Stock).IsRequired();
            // 16 char , 2 char = 18 char
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.ToTable("Product");
            // CategoryId for EF Core name rules
            builder.HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);
        }
    }
}
