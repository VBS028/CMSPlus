using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces;

namespace CMSPlus.Application.Extentions;

public static class CommentExtentions
{

    public static IVisitorService VisitorService;
    private static List<string> _list = new List<string>();
    public static void Accept(this BlogCommentEntity comment, BlogCommentEntity reply)
    {
        Notify(comment, reply);
        if (comment.ParentComment != null)
        {
            Accept(comment.ParentComment, reply);
        }
        
        _list.Clear();
    }

    public static async Task Notify(BlogCommentEntity comment, BlogCommentEntity reply)
    {
        if (!_list.Contains(comment.Username))
        {
            await VisitorService.Notify(comment, reply);
            _list.Add(comment.Username);
        }
    }
}