using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Infrastructure.Enums;
using CMSPlus.Presentation.Models.BlogModels;

namespace CMSPlus.Presentation.Adapters;

public class BlogAdapter : IAdapter
{
    public EntityTypes Type => EntityTypes.Blog;

    public object GetData(object source)
    {
        if (!(source is TopicEntity topicEntity))
            throw new ArgumentException("Invalid source type. Expected TopicEntity.");

        return new BlogEntity()
        {
            Body = topicEntity.Body,
            SystemName = topicEntity.SystemName,
            Title = topicEntity.Title
        };
    }
}