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
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfor>
    {
        public void Configure(EntityTypeBuilder<UserInfor> builder)
        {
            builder.ToTable("UserInfors");
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.User)
                .WithMany(p => p.UserInfors)
                .HasForeignKey(o=>o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
