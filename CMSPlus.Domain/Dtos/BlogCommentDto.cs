using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Domain.Dtos
{
    public class BlogCommentDto
    {
        public int BlogId { get; set; }
        public string Username { get; set; }
        public string Body { get; set; }
    }
}
