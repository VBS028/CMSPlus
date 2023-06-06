using CMSPlus.Domain.Dtos;
using CMSPlus.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Domain.Interfaces.Builders
{
    public interface IBlogBuilder
    {
        IBlogBuilder WithTitle(string title);
        IBlogBuilder WithBody(string body);
        IBlogBuilder WithSystemName(string systemName);
        IBlogBuilder WithAuthor(string? author);
        IBlogBuilder From(BlogEntity blogEntity);
        BlogEntity Build();
    }
}

