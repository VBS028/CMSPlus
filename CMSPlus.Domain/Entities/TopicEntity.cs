namespace CMSPlus.Domain.Entities;

public class TopicEntity:BaseEntity
{
    public string SystemName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string CreatorId { get; set; }
}
