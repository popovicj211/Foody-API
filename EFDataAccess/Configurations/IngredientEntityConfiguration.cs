using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDataAccess.Configurations
{
    public class IngredientEntityConfiguration : IEntityTypeConfiguration<IngredientEntity>
    {
        public void Configure(EntityTypeBuilder<IngredientEntity> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            #endregion Properties

            #region Relations

            builder.HasMany(u => u.DishIngredients)
                   .WithOne(g => g.Ingredient)
                   .HasPrincipalKey(u => u.Id)
                   .HasForeignKey(g => g.IngreId);

            #endregion Relations
        }
    }
}
