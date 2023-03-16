using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace EFDataAccess.Configurations
{
    public class DishCommentEntityConfiguration : IEntityTypeConfiguration<DishCommentEntity>
    {
        public void Configure(EntityTypeBuilder<DishCommentEntity> builder)
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
                  .WithMany(g => g.DishComments)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasPrincipalKey(g => g.Id)
                  .HasForeignKey(gg => gg.DishId)
                  .IsRequired();

            builder.HasOne(gg => gg.Comment)
                   .WithMany(g => g.DishComments)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(gg => gg.CommentId)
                   .IsRequired();

            #endregion Relations
        }
    }
}
