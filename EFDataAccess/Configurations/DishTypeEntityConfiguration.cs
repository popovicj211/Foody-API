using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDataAccess.Configurations
{
    public class DishTypeEntityConfiguration : IEntityTypeConfiguration<DishTypeEntity>
    {
        public void Configure(EntityTypeBuilder<DishTypeEntity> builder)
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

            builder.HasMany(u => u.DishTypeDishes)
                   .WithOne(g => g.DishType)
                   .HasPrincipalKey(u => u.Id)
                   .HasForeignKey(g => g.DishTypeId);

            #endregion Relations
        }
    }
}
