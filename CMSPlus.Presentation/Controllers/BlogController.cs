using AutoMapper;
using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Models.TopicModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CMSPlus.Domain.Interfaces.Services;
using CMSPlus.Domain.Interfaces;

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
    private readonly IExtendedEmailService _extendedEmailService;
    private readonly IFileService _fileService;
    private readonly IBlogCommentsService _blogCommentsService;


    public BlogController(
        IBlogService blogService,
        IMapper mapper,
        UserManager<IdentityUser> userManager,
        IValidator<BlogEditViewViewModel> editModelValidator,
        IValidator<BlogCreateViewModel> createModelValidator,
        IValidator<BlogCommentCreateViewModel> commentValidator,
        IConfiguration configuration,
        IExtendedEmailService extendedEmailService,
        IFileService fileService,
        IBlogCommentsService blogCommentsService)
    {
        _blogService = blogService;
        _mapper = mapper;
        _userManager = userManager;
        _editModelValidator = editModelValidator;
        _createModelValidator = createModelValidator;
        _commentValidator = commentValidator;
        _configuration = configuration;
        _extendedEmailService = extendedEmailService;
        _fileService = fileService;
        _blogCommentsService = blogCommentsService;
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

        var userId = _userManager.GetUserId(this.User);
        if (userId != null)
        {
            blog.CreatorId = userId;
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
        blogViewModel.Comments = _mapper.Map<IEnumerable<BlogCommentEntity>, IEnumerable<BlogCommentViewModel>>(blogComments);
        return View(blogViewModel);
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
        comment.Username = identityUser.UserName;
        var commentEntity = _mapper.Map<BlogCommentCreateViewModel, BlogCommentEntity>(comment);
        await _blogCommentsService.Create(commentEntity);
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
                await _extendedEmailService.SendEmailWithAttachmentAsync(adminsEmails,"blog attachment","some body",zipPath);
            }
        }
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
