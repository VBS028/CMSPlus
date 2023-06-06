using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Domain.Dtos
{
    public class BlogCommentWithAttachmentDto:BlogCommentDto
    {
        public IEnumerable<IFormFile>? Files { get; set; }
    }
}
