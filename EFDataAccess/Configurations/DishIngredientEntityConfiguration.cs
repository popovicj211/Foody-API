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
    public class DishIngredientEntityConfiguration : IEntityTypeConfiguration<DishIngredientEntity>
    {
        public void Configure(EntityTypeBuilder<DishIngredientEntity> builder)
        {
            builder.HasKey(gg => gg.Id);

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            #endregion Properties

            #region Relations

            builder.HasOne(gg => gg.Dish)
                  .WithMany(g => g.DishIngredients)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasPrincipalKey(g => g.Id)
                  .HasForeignKey(gg => gg.DishId)
                  .IsRequired();

            builder.HasOne(gg => gg.Ingredient)
                   .WithMany(g => g.DishIngredients)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(gg => gg.IngreId)
                   .IsRequired();

            #endregion Relations
        }
    }
}
