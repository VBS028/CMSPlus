using CMSPlus.Domain.Models;

namespace CMSPlus.Presentation.Models.BlogModels;

public class BlogCreateViewModel : BaseCreateViewModel
{
    public string? Author { get; set; }
}