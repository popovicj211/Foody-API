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
    public class CommentEntityConfiguration : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.HasKey(gg => gg.Id);
            builder.HasIndex(gg => gg.ParentId);

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Content).IsRequired().HasColumnType("ntext");
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired(false).HasDefaultValueSql(null);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            #endregion Properties

            #region Relations

            builder.HasOne(c => c.Parent)
                   .WithMany(c => c.SubComments)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(c => c.ParentId);

            builder.HasOne(gg => gg.User)
                   .WithMany(g => g.Comments)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(gg => gg.UserId)
                   .IsRequired();

            builder.HasMany(g => g.DishComments)
                   .WithOne(gg => gg.Comment)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(gg => gg.CommentId);

            #endregion Relations
        }
    }
}
