using CMSPlus.Domain.Entities;

namespace CMSPlus.Domain.Interfaces;

public interface ITopicService : IService<TopicEntity>
{
    public Task<TopicEntity?> GetBySystemName(string systemName);
}