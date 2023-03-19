using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces.Repositories;
using CMSPlus.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CMSPlus.Domain.Repositories;

public class BlogRepository : Repository<BlogEntity>, IBlogRepository
{
    public BlogRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<BlogEntity?> GetBySystemName(string systemName)
    {
        var result = await Table.SingleOrDefaultAsync(blog => blog.SystemName == systemName);
        return result;
    }
}
