namespace CMSPlus.Presentation.Models.BlogModels;

public class BlogCommentViewModel
{
    public BlogCommentViewModel()
    {
        UpdatedOnUtc = CreatedOnUtc = DateTime.UtcNow;
    }

    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Username { get; set; }
    public string Body { get; set; }
    public DateTime? CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
}