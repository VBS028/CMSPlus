namespace CMSPlus.Domain.Entities;

public class BlogEntity:BaseEntity
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string SystemName { get; set; }
    public virtual ICollection<BlogCommentEntity> BlogComments { get; set; }
    public string CreatorId { get; set; }
}