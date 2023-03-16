using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Configurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.HasKey(gg => gg.Id);

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Qty).IsRequired();
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            #endregion Properties

            #region Relations

            builder.HasOne(gg => gg.Order)
                  .WithMany(g => g.OrderItems)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasPrincipalKey(g => g.Id)
                  .HasForeignKey(gg => gg.OrderId)
                  .IsRequired(true);

            builder.HasOne(gg => gg.Dish)
                   .WithMany(g => g.OrderItems)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(gg => gg.DishId)
                   .IsRequired();

            #endregion Relations
        }
    }
}
