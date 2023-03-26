using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDataAccess.Configurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.TotalPrice).IsRequired().HasPrecision(11, 2).HasColumnType("decimal(11, 2)");
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            #endregion Properties

            #region Relations

            builder.HasMany(u => u.OrderItems)
                   .WithOne(g => g.Order)
                   .HasPrincipalKey(u => u.Id)
                   .HasForeignKey(g => g.OrderId);

            builder.HasOne(gg => gg.User)
                .WithMany(g => g.Orders)
                .HasPrincipalKey(g => g.Id)
                .HasForeignKey(gg => gg.UserId)
                .IsRequired();

            #endregion Relations
        }
    }
}
