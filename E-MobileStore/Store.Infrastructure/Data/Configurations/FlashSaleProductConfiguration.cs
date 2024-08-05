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
    public class FlashSaleProductConfiguration : IEntityTypeConfiguration<FlashSaleProduct>
    {
        public void Configure(EntityTypeBuilder<FlashSaleProduct> builder)
        {

            builder.ToTable("FlashSaleProducts");
            builder.HasKey(fsp => new { fsp.FlashSaleId, fsp.ProductId });
            builder.HasOne(fsp => fsp.FlashSale)
                    .WithMany(fs => fs.FlashSaleProducts)
                    .HasForeignKey(fsp => fsp.FlashSaleId)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(fsp => fsp.Product)
                    .WithMany(p => p.FlashSaleProducts)
                    .HasForeignKey(fsp => fsp.ProductId);
        }
    }
}
