using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces.Builders;
using Microsoft.AspNetCore.Http;

namespace CMSPlus.Application.Builders
{
    public class BlogBuilder:IBlogBuilder
    {
        private string _title;
        private string _body;
        private string _systemName;
        private string? _author;
        private IEnumerable<IFormFile> _files;

        public IBlogBuilder WithAuthor(string? author)
        {
            _author = author;
            return this;
        }

        public IBlogBuilder From(BlogEntity blog)
        {
            _title = blog.Title;
            _body = blog.Body;
            _systemName = blog.SystemName;
            _author = blog.Author;
            return this;
        }

        public IBlogBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public IBlogBuilder WithBody(string body)
        {
            _body = body;
            return this;
        }

        public IBlogBuilder WithSystemName(string systemName)
        {
            _systemName = systemName;
            return this;
        }

        public BlogEntity Build()
        {
            return new BlogEntity
            {
                Title = _title,
                Body = _body,
                SystemName = _systemName,
                Author = _author
            };
        }
    }
}
