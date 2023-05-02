using CMSPlus.Domain.Entities;

namespace CMSPlus.Presentation.Extentions;

public static class BlogExtentions
{
    public static BlogEntity WithAuthor(this BlogEntity blog, string author)
    {
        blog.Author = author;
        return blog;
    }
}