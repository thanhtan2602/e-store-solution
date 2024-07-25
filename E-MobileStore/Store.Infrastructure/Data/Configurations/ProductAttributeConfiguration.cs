using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Data.Configurations
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {

        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttributes");
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.Product)
                .WithMany(o => o.ProductAttributes)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.AttributeValue)
                .WithMany(o => o.ProductAttributes)
                .HasForeignKey(o => o.AttributeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
