using CMSPlus.Domain.Entities;

namespace CMSPlus.Domain.Interfaces;

public interface IVisitorService
{
    Task Notify(BlogCommentEntity username, BlogCommentEntity reply);
}