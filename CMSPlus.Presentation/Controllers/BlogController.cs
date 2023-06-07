using System.Security.Cryptography;
using AutoMapper;
using CMSPlus.Application.Extentions;
using CMSPlus.Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CMSPlus.Domain.Interfaces.Services;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Presentation.Models.BlogModels;
using CMSPlus.Domain.Interfaces.Builders;
using CMSPlus.Domain.Interfaces.Factories;
using CMSPlus.Domain.Dtos;
using CMSPlus.Presentation.Extentions;

namespace CMSPlus.Presentation.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly IValidator<BlogCreateViewModel> _createModelValidator;
    private readonly IValidator<BlogEditViewViewModel> _editModelValidator;
    private readonly IValidator<BlogCommentCreateViewModel> _commentValidator;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IExtendedEmailSender _extendedEmailSender;
    private readonly IFileService _fileService;
    private readonly IBlogCommentsService _blogCommentsService;
    private readonly IBlogBuilder _blogBuilder;


    public BlogController(
        IBlogService blogService,
        IMapper mapper,
        UserManager<IdentityUser> userManager,
        IValidator<BlogEditViewViewModel> editModelValidator,
        IValidator<BlogCreateViewModel> createModelValidator,
        IValidator<BlogCommentCreateViewModel> commentValidator,
        IConfiguration configuration,
        IExtendedEmailSender extendedEmailSender,
        IFileService fileService,
        IBlogCommentsService blogCommentsService,IBlogBuilder blogBuilder)
    {
        _blogService = blogService;
        _mapper = mapper;
        _userManager = userManager;
        _editModelValidator = editModelValidator;
        _createModelValidator = createModelValidator;
        _commentValidator = commentValidator;
        _configuration = configuration;
        _extendedEmailSender = extendedEmailSender;
        _fileService = fileService;
        _blogCommentsService = blogCommentsService;
        _blogBuilder = blogBuilder;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var blogs = await _blogService.GetAll();
        var blogsViewModel = _mapper.Map<IEnumerable<BlogEntity>, IEnumerable<BlogGetViewViewModel>>(blogs);
        return View(blogsViewModel);
    }
    
    [Authorize(Permissions.Blog.Create)]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [Authorize(Permissions.Blog.Create)]
    [HttpPost]
    public async Task<IActionResult> Create(BlogCreateViewModel blog)
    {
        var validationResult = await _createModelValidator.ValidateAsync(blog);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View(blog);
        }

        var user = await _userManager.GetUserAsync(this.User);
        if (user != null)
        {
            blog.CreatorId = user.Id;
            blog.Author = user.UserName;
        }
        var blogEntity = _mapper.Map<BlogCreateViewModel, BlogEntity>(blog);
        await _blogService.Create(blogEntity);
        return RedirectToAction("Index");
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(int id,int pageIndex=0, int pageSize=3)
    {
        var blog = await _blogService.GetById(id);
        var blogComments = await _blogCommentsService.GetCommentsByBlogId(id);
        if (blog == null)
        {
            throw new ArgumentException($"Item with id: {id} wasn't found!");
        }
        var blogViewModel = _mapper.Map<BlogEntity, BlogGetViewViewModel>(blog);
        blogViewModel.Comments = blogComments.Select(x => BuildCommentTree(x, 0));
        return View(blogViewModel);
    }

    private BlogCommentViewModel BuildCommentTree(BlogCommentEntity commentViewEntity, int level)
    {
        var commentViewModel = _mapper.Map<BlogCommentEntity, BlogCommentViewModel>(commentViewEntity);
        commentViewModel.Replies = new List<BlogCommentViewModel>();
        commentViewModel.Level = level;

        foreach (var reply in commentViewEntity.Replies)
        {
            commentViewModel.Replies.Add(BuildCommentTree(reply, level + 1));
        }

        return commentViewModel;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Details(BlogCommentCreateViewModel comment)
    {
        var user = this.User;
        var identityUser = await _userManager.GetUserAsync(user);
        if (identityUser == null)
        {
            return RedirectToPage("/Account/Login", new {area="Identity"});
        }

        var commentEntity = _mapper.Map<BlogCommentCreateViewModel, BlogCommentEntity>(comment);
        commentEntity.Username = identityUser.UserName;
        commentEntity.ParentCommentId = comment.ParentCommentId > 0 ? comment.ParentCommentId : null;
        if (commentEntity.ParentCommentId != null)
        {
            var parent = await _blogCommentsService.GetById(commentEntity.ParentCommentId.Value);
            parent.Accept(commentEntity);
        }
        if (comment.Files != null)
        {
            var rootPath = _configuration.GetValue<string>("AttachmentsPath");

            var blogAttachmentsPath = Path.Combine(
                rootPath,
                $"Blog-{comment.BlogId}");
            var zipName = $"Blog-{comment.BlogId}_Comment-{commentEntity.Id}.zip";
            var zipPath = await _fileService.ZipFiles(comment.Files, blogAttachmentsPath, zipName);
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var adminsEmails = admins.Select(x => x.Email).ToList();
            if (!adminsEmails.IsNullOrEmpty())
            {
                await _extendedEmailSender.SendEmailAsync(string.Join('|', adminsEmails), "blog attachment", "some body", zipPath);
            }
        }
        else
        {

        }
        await _blogCommentsService.Create(commentEntity);
        return RedirectToAction("Details",comment.BlogId);
    }

    [Authorize(Permissions.Blog.Edit)]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var blogToEdit = await _blogService.GetById(id);
        if (blogToEdit == null)
        {
            throw new ArgumentException($"Item with Id: {id} wasn't found!");
        }
        var blogViewModel = _mapper.Map<BlogEntity, BlogEditViewViewModel>(blogToEdit);
        return View(blogViewModel);
    }
    
    [Authorize(Permissions.Blog.Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(BlogEditViewViewModel updatedEntity)
    {
        var validationResult = await _editModelValidator.ValidateAsync(updatedEntity);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View(updatedEntity);
        }

        var topicEntity = await _blogService.GetById(updatedEntity.Id);
        topicEntity = _mapper.Map(updatedEntity, topicEntity);
        await _blogService.Update(topicEntity);
        return RedirectToAction("Index");
    }

    [Authorize(Permissions.Blog.Create)]
    public async Task<IActionResult> Clone(BlogEntity source)
    {
        var copy = (BlogEntity)source.Clone();
        var user = this.User;
        var identityUser = await _userManager.GetUserAsync(user);
        if (!string.IsNullOrEmpty(identityUser?.UserName))
        {
            copy = _blogBuilder.From(copy).WithAuthor(identityUser.UserName).Build();
        }
        await _blogService.Create(copy);

        return RedirectToAction("Details", new {id=copy.Id});
    }
    
    [Authorize(Permissions.Blog.Delete)]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var blogToDelete = await _blogService.GetById(id);
        if (blogToDelete == null)
        {
            throw new ArgumentException($"Item with Id: {id} wasn't found!");
        }

        var blogViewModel = _mapper.Map<BlogEntity, BlogEditViewViewModel>(blogToDelete);
        return View(blogViewModel);
    }    
    
    [Authorize(Permissions.Blog.Delete)]
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _blogService.Delete(id);
        return RedirectToAction("Index");
    }
}
