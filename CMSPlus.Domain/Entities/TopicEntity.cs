using CMSPlus.Domain.Interfaces;

namespace CMSPlus.Domain.Entities;

public class TopicEntity:BaseEntity,IClonable<TopicEntity>
{

    public TopicEntity()
    {

    }

    public TopicEntity(TopicEntity other):base()
    {
        SystemName = other.SystemName;
        Title= other.Title;
        Body = other.Body;
    }

    public string SystemName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;

    public TopicEntity Clone()
    {
        return new TopicEntity(this);
    }
}
