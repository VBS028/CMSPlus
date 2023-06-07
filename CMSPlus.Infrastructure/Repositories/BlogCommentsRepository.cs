using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces.Repositories;
using CMSPlus.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CMSPlus.Domain.Repositories;

public class BlogCommentsRepository:Repository<BlogCommentEntity>,IBlogCommentsRepository
{
    public BlogCommentsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<BlogCommentEntity>> GetBlogCommentsById(int blogPostId)
    {
        return await Table
        .Where(x => x.BlogId == blogPostId && x.ParentCommentId == null)
        .OrderByDescending(it => it.UpdatedOnUtc).ToListAsync();
    }
}