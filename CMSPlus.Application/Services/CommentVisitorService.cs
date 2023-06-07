using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CMSPlus.Application.Services;

public class CommentVisitorService:IVisitorService
{
    private readonly IEmailSender _emailSender;
    private readonly UserManager<IdentityUser> _userManager;

    public CommentVisitorService(IEmailSender emailSender, UserManager<IdentityUser> userManager)
    {
        _emailSender = emailSender;
        _userManager = userManager;
    }

    public async Task Notify(BlogCommentEntity comment, BlogCommentEntity reply)
    {
        var user = await _userManager.FindByEmailAsync(comment.Username);
        if (user != null)
        {
            await _emailSender.SendEmailAsync(user.Email, "reply", $"New reply from ${reply.Username}:{reply.Body} on Blog:{comment.Blog.Title} conversation");
        }
    }
}