using CMSPlus.Domain.Entities;

namespace CMSPlus.Domain.Interfaces.Repositories;

public interface IBlogCommentsRepository : IRepository<BlogCommentEntity>
{
    public Task<IEnumerable<BlogCommentEntity>> GetBlogCommentsById(int id);
}