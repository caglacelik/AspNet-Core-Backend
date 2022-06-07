using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Seed
    // tablo içine default datalar atılması için
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // id leri de eklemek zorunlu -- seed data için
            // Category c = new(); c#10
            builder.HasData(new Category { Id = 1, Name = "Kalem" },
                new Category { Id = 2, Name = "Kitap" },
                new Category { Id = 3, Name = "Defter" });
        }
    }
}
