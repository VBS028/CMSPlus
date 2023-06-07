using Microsoft.AspNetCore.Http;

namespace CMSPlus.Presentation.Models.BlogModels;

public class BlogCommentCreateViewModel
{
    public int BlogId { get; set; }
    public string Username { get; set; }
    public string Body { get; set; }
    public int? ParentCommentId { get; set; }
    public IEnumerable<IFormFile>? Files { get; set; }
}