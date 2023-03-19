using CMSPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSPlus.Domain.Configurations;

public class TopicEntityConfiguration:IEntityTypeConfiguration<TopicEntity>
{
    public void Configure(EntityTypeBuilder<TopicEntity> builder)
    {
        builder.ToTable("Topics");
        builder.Property(x => x.Body).IsRequired();
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.SystemName).IsRequired();
    }
}