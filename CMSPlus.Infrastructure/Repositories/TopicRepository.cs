using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces.Repositories;
using CMSPlus.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CMSPlus.Domain.Repositories;

public class TopicRepository:Repository<TopicEntity>,ITopicRepository
{
    public TopicRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<TopicEntity?> GetBySystemName(string systemName)
    {
        var result = await Table.SingleOrDefaultAsync(topic => topic.SystemName == systemName);
        return result;
    }
}