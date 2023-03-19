using CMSPlus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Domain.Interfaces
{
    public interface IService<T>
    {
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task Create(T entity);
        public Task Update(T entity);
        public Task Delete(int id);
    }
}
