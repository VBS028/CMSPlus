using CMSPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSPlus.Domain.Configurations;

public class BlogCommentEntityConfiguration:IEntityTypeConfiguration<BlogCommentEntity>
{
    public void Configure(EntityTypeBuilder<BlogCommentEntity> builder)
    {
        builder.ToTable("BlogComments");
        builder.Property(x => x.Body).IsRequired();

        builder
            .HasOne(b => b.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_BlogComments_BlogComments_ParentCommentId")
            .IsRequired(false);

        builder
            .HasOne(b => b.Blog)
            .WithMany(c => c.BlogComments)
            .HasForeignKey(c => c.BlogId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_BlogComments_Blogs_BlogId");
    }
}