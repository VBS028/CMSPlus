using CMSPlus.Domain.Entities;

namespace CMSPlus.Domain.Interfaces.Repositories;

public interface IBlogRepository : IRepository<BlogEntity>
{
    public Task<BlogEntity?> GetBySystemName(string systemName);
}