using CMSPlus.Domain.Interfaces;

namespace CMSPlus.Domain.Entities;

public class BlogEntity:BaseEntity, IClonable<BlogEntity>
{
    public BlogEntity()
    {

    }

    public BlogEntity(BlogEntity other): base()
    {
        Title= other.Title;
        Body= other.Body;
        SystemName= other.SystemName;
    }

    public string Title { get; set; }
    public string Body { get; set; }
    public string SystemName { get; set; }
    public string? Author { get; set; }
    public virtual ICollection<BlogCommentEntity> BlogComments { get; set; }

    public BlogEntity Clone()
    {
        return new BlogEntity(this);
    }
}