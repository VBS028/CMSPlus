using CMSPlus.Domain.Entities;

namespace CMSPlus.Domain.Interfaces
{
    public interface IBlogCommentsService:IService<BlogCommentEntity>
    {
        public Task<IEnumerable<BlogCommentEntity>> GetCommentsByBlogId(int id);
    }
}
