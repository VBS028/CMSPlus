using CMSPlus.Domain.Dtos;
using CMSPlus.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Domain.Interfaces.Factories
{
    public interface IBlogCommentFactory
    {
        BlogCommentDto CreateBlogComment(int blogId, string username, string body);
        BlogCommentWithAttachmentDto CreateBlogCommentWithAttachments(int blogId, string username, string body, List<IFormFile> attachments);
    }
}
