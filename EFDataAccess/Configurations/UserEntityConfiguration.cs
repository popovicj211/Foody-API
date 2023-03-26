using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDataAccess.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Index

            builder.HasIndex(g => new { g.Username , g.Email }).IsUnique(true);

            #region Properties

            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.FirstName).IsRequired().HasColumnType("nvarchar(30)"); 
            builder.Property(u => u.LastName).IsRequired().HasColumnType("nvarchar(30)");
            builder.Property(u => u.Username).IsRequired().HasColumnType("nvarchar(25)");
            builder.Property(u => u.Email).IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(u => u.Password).HasMaxLength(300).IsRequired();
            builder.Property(r => r.ImagePath).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.Token).IsRequired(false).HasColumnType("nvarchar(100)");
            builder.Property(u => u.IsActived).HasDefaultValue(true);
            builder.Property(u => u.RoleId).IsRequired().HasDefaultValue(2);
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            #endregion Properties

            #region Relations

            builder.HasOne(g => g.Role)
                   .WithMany(u => u.Users)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasPrincipalKey(p => p.Id)
                   .HasForeignKey(g => g.RoleId)
                   .IsRequired();

            #endregion Relations
        }
    }
}
