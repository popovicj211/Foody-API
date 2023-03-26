using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDataAccess.Configurations
{
    public class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.HasKey(gg => gg.Id);

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.FullName).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(r => r.Email).IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(r => r.Message).IsRequired().HasColumnType("ntext");
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            #endregion Properties
        }
    }
}
