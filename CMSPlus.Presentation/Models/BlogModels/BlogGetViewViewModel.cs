namespace CMSPlus.Domain.Models.TopicModels;

public class BlogGetViewViewModel:BaseViewModel
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string SystemName { get; set; }
    public IEnumerable<BlogCommentViewModel>? Comments { get; set; }
}