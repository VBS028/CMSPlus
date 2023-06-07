using AutoMapper;
using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Domain.Interfaces.Services;
using CMSPlus.Domain.Models.TopicModels;
using CMSPlus.Infrastructure.Enums;
using CMSPlus.Presentation.Adapters;
using CMSPlus.Presentation.Models.BlogModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IEmailSender = Microsoft.AspNetCore.Identity.UI.Services.IEmailSender;

namespace CMSPlus.Presentation.Controllers;

public class TopicController : Controller
{
    private readonly ITopicService _topicService;
    private readonly IMapper _mapper;
    private readonly IValidator<TopicEditViewViewModel> _editModelValidator;
    private readonly IValidator<TopicCreateViewModel> _createModelValidator;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IBlogService _blogService;
    private readonly IStrategyResolver<IAdapter> _adapterResolver;


    public TopicController(
        ITopicService topicService,
        IMapper mapper, 
        IValidator<TopicEditViewViewModel> editModelValidator, 
        IValidator<TopicCreateViewModel> createModelValidator, 
        IEmailSender emailSender, 
        UserManager<IdentityUser> userManager, IBlogService blogService, IStrategyResolver<IAdapter> adapterResolver)
    {
        _topicService = topicService;
        _mapper = mapper;
        _editModelValidator = editModelValidator;
        _createModelValidator = createModelValidator;
        _emailSender = emailSender;
        _userManager = userManager;
        _blogService = blogService;
        _adapterResolver = adapterResolver;
    }
    
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var topics =  await _topicService.GetAll();
        var topicToDisplay = _mapper.Map<IEnumerable<TopicEntity>, IEnumerable<TopicViewViewModel>>(topics);
        return View(topicToDisplay);
    }
    
    [Authorize(Permissions.Topic.Edit)]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var topicToEdit = await _topicService.GetById(id);
        if (topicToEdit == null)
        {
            throw new ArgumentException($"Item with Id: {id} wasn't found!");
        }
        var topicViewModel = _mapper.Map<TopicEntity, TopicEditViewViewModel>(topicToEdit);
        return View(topicViewModel);
    }
    
    [Authorize(Permissions.Topic.Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(TopicEditViewViewModel updatedEntity)
    {
        var validationResult = await _editModelValidator.ValidateAsync(updatedEntity);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View(updatedEntity);
        }

        var topicEntity = await _topicService.GetById(updatedEntity.Id);
        topicEntity = _mapper.Map(updatedEntity, topicEntity);
        await _topicService.Update(topicEntity);
        return RedirectToAction("Index");
    }
    
    [Authorize(Permissions.Topic.Delete)]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var topicToDelete = await _topicService.GetById(id);
        if (topicToDelete == null)
        {
            throw new ArgumentException($"Item with Id: {id} wasn't found!");
        }
        var topicViewModel = _mapper.Map<TopicEntity, TopicViewViewModel>(topicToDelete);
        return View(topicViewModel);
    }    
    
    [Authorize(Permissions.Topic.Delete)]
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _topicService.Delete(id);
        return RedirectToAction("Index");
    }
    
    [Authorize(Permissions.Topic.Create)]
    [HttpGet]
    public async Task<IActionResult>Create()
    {
        return View();
    }
    
    
    [Authorize(Permissions.Topic.Create)]
    [HttpPost]
    public async Task<IActionResult> Create(TopicCreateViewModel topic)
    {
        var validationResult = await _createModelValidator.ValidateAsync(topic);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View(topic);
        }
        var userId = _userManager.GetUserId(this.User);
        if (userId != null)
        {
            topic.CreatorId = userId;
        }
        var topicEntity = _mapper.Map<TopicCreateViewModel, TopicEntity>(topic);
        await _topicService.Create(topicEntity);
        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    [Route("Topic/Details/{systemName}")]
    [HttpGet]
    public async Task<IActionResult> Details(string systemName)
    {
        var topic = await _topicService.GetBySystemName(systemName);
        if (topic == null)
        {
            throw new ArgumentException($"Item with system name: {systemName} wasn't found!");
        }
        var topicViewModel = _mapper.Map<TopicEntity, TopicDetailsViewViewModel>(topic);
        return View(topicViewModel);
    }
    
    [Authorize(Permissions.Topic.GetEmail)]
    [HttpPost]
    [ActionName("Details")]
    public async Task<IActionResult> DetailsToEmail(TopicDetailsViewViewModel model)
    {
        var user = this.User;
        if (user == null)
        {
            RedirectToPage("/Account/Login", new {area="Identity"});
        }

        var topic = _mapper.Map<TopicDetailsViewViewModel, TopicEntity>(model);
        var json = _adapterResolver.GetStrategy(EntityTypes.Json).GetData(topic).ToString();
        var identityUser = await _userManager.GetUserAsync(user);
        await _emailSender.SendEmailAsync(identityUser.Email,"Test email",json);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> CopyAsBlog(int id)
    {
        var topic = await _topicService.GetById(id);

        var blog = _adapterResolver.GetStrategy(EntityTypes.Blog).GetData(topic) as BlogEntity;

        await _blogService.Create(blog);

        return RedirectToAction("Details", "Blog", new { id = blog.Id });
    }
}