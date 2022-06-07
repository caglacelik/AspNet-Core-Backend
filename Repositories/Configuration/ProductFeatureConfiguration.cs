﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Configuration
{
    internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            // One to One relation for EF Core
            builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x=>x.ProductId);

        }
    }
}
