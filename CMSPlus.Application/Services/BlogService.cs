using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces.Repositories;
using CMSPlus.Domain.Interfaces.Services;

namespace CMSPlus.Application.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _repository;

    public BlogService(IBlogRepository repository)
    {
        _repository = repository;
    }

    public async Task<BlogEntity> GetById(int id)
    {
        if (id == 0)
            throw new ArgumentException();
        return await _repository.GetById(id);
    }

    public async Task<BlogEntity?> GetBySystemName(string systemName)
    {
        if (string.IsNullOrEmpty(systemName))
            throw new ArgumentException();
        return await _repository.GetBySystemName(systemName);
    }

    public async Task<IEnumerable<BlogEntity>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task Create(BlogEntity entity)
    {
        await _repository.Create(entity);
    }

    public async Task Update(BlogEntity entity)
    {
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
}