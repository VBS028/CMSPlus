using CMSPlus.Domain.Entities;

namespace CMSPlus.Domain.Interfaces.Services;

public interface IBlogService : IService<BlogEntity>
{
    public Task<BlogEntity?> GetBySystemName(string systemName);
}