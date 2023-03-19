using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Application.Services
{
    public class BlogCommentService:IBlogCommentsService
    {
        private readonly IBlogCommentsRepository _repository;

        public BlogCommentService(IBlogCommentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<BlogCommentEntity> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<BlogCommentEntity>> GetCommentsByBlogId(int id)
        {
            return await _repository.GetBlogCommentsById(id);
        }

        public async Task<IEnumerable<BlogCommentEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Create(BlogCommentEntity entity)
        {
            await _repository.Create(entity);
        }

        public async Task Update(BlogCommentEntity entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
