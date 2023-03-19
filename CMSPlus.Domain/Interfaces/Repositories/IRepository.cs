namespace CMSPlus.Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(int id);
    public Task Create(T entity);
    public Task Update(T entity);
    public Task Delete(int id);
}