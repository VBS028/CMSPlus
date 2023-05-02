using CMSPlus.Application.Builders;
using CMSPlus.Domain.Dtos;
using CMSPlus.Domain.Interfaces.Builders;
using CMSPlus.Domain.Interfaces.Factories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogBuilder = CMSPlus.Application.Builders.BlogBuilder;

namespace CMSPlus.Application.Factories
{
    public class BlogCommentFactory : IBlogCommentFactory
    {
        public BlogCommentDto CreateBlogComment(int blogId, string username, string body)
        {
            return new BlogCommentDto
            {
                BlogId = blogId,
                Username = username,
                Body = body
            };
        }

        public BlogCommentWithAttachmentDto CreateBlogCommentWithAttachments(int blogId, string username, string body, List<IFormFile> attachments)
        {
            return new BlogCommentWithAttachmentDto
            {
                BlogId = blogId,
                Username = username,
                Body = body,
                Files = attachments,
            };
        }
    }
}
