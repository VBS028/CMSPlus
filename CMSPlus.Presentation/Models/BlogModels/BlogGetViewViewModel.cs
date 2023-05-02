using CMSPlus.Domain.Models;

namespace CMSPlus.Presentation.Models.BlogModels;

public class BlogGetViewViewModel : BaseViewModel
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string SystemName { get; set; }
    public string Author { get; set; }
    public IEnumerable<BlogCommentViewModel>? Comments { get; set; }
}