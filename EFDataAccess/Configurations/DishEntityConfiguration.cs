using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Configurations
{
    public class DishEntityConfiguration : IEntityTypeConfiguration<DishEntity>
    {
        public void Configure(EntityTypeBuilder<DishEntity> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(r => r.ImagePath).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.Description).HasColumnType("ntext");
            builder.Property(r => r.Price).IsRequired().HasPrecision(11,2).HasColumnType("decimal(11,2)");
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            #endregion Properties

            #region Relations

            builder.HasMany(g => g.DishTypeDishes)
            .WithOne(gg => gg.Dish)
            .HasPrincipalKey(g => g.Id)
            .HasForeignKey(gg => gg.DishId);

            builder.HasMany(g => g.DishIngredients)
                .WithOne(gg => gg.Dish)
                .HasPrincipalKey(g => g.Id)
                .HasForeignKey(gg => gg.DishId);

            builder.HasMany(g => g.DishComments)
              .WithOne(gg => gg.Dish)
              .HasPrincipalKey(g => g.Id)
              .HasForeignKey(gg => gg.DishId);

            builder.HasMany(g => g.OrderItems)
              .WithOne(gg => gg.Dish)
              .HasPrincipalKey(g => g.Id)
              .HasForeignKey(gg => gg.DishId);

            #endregion Relations
        }
    }
}
