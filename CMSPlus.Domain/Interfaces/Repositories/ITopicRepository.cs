using CMSPlus.Domain.Entities;

namespace CMSPlus.Domain.Interfaces.Repositories;

public interface ITopicRepository : IRepository<TopicEntity>
{
    public Task<TopicEntity?> GetBySystemName(string systemName);
}