using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShope.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Core.FluentAPIConfiguration
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(s=>s.Id);
            builder.Property(p => p.ProductName)
                .IsRequired()
                .HasColumnName("Titel")
                .HasMaxLength(256)
                .HasColumnOrder(1);
        }
    }
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(s => s.Id);
            builder.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(64);
        }
    }

}
