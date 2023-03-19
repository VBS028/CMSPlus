namespace CMSPlus.Domain.Entities;

public class BlogCommentEntity : BaseEntity
{
    public string Username { get; set; }
    public string Body { get; set; }
    public virtual BlogEntity Blog { get; set; }
    public int BlogId { get; set; }
}