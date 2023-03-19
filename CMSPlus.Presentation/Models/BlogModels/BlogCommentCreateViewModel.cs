using Microsoft.AspNetCore.Http;

namespace CMSPlus.Domain.Models.TopicModels;

public class BlogCommentCreateViewModel
{
    public int BlogId { get; set; }
    public string Username { get; set; }
    public string Body { get; set; }
    public IEnumerable<IFormFile>? Files{ get; set; }
}