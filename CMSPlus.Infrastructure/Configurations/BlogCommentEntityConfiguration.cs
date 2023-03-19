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
        builder.HasOne(d => d.Blog).WithMany(p => p.BlogComments)
            .HasForeignKey(d => d.BlogId);
    }
}