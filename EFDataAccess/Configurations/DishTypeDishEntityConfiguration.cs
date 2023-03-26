using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFDataAccess.Configurations
{
    public class DishTypeDishEntityConfiguration : IEntityTypeConfiguration<DishTypeDishEntity>
    {
        public void Configure(EntityTypeBuilder<DishTypeDishEntity> builder)
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
                  .WithMany(g => g.DishTypeDishes)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasPrincipalKey(g => g.Id)
                  .HasForeignKey(gg => gg.DishId)
                  .IsRequired();

            builder.HasOne(gg => gg.DishType)
                   .WithMany(g => g.DishTypeDishes)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(gg => gg.DishTypeId)
                   .IsRequired();

            #endregion Relations
        }
    }
}
