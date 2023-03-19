using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Domain.Interfaces.Repositories;

namespace CMSPlus.Application.Services;

public class TopicService : ITopicService
{
    private readonly ITopicRepository _repository;

    public TopicService(ITopicRepository repository)
    {
        _repository = repository;
    }

    public async Task<TopicEntity> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<TopicEntity?> GetBySystemName(string systemName)
    {
        return await _repository.GetBySystemName(systemName);
    }

    public async Task<IEnumerable<TopicEntity>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task Create(TopicEntity entity)
    {
        await _repository.Create(entity);
    }

    public async Task Update(TopicEntity entity)
    {
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
}