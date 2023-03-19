using CMSPlus.Domain.Models;
using CMSPlus.Domain.Models.TopicModels;
using CMSPlus.Domain.Interfaces.Services;
using CMSPlus.Domain.Interfaces;

namespace CMSPlus.Presentation.Validations.Helpers;

public class ValidatorHelpers
{
    private readonly ITopicService _topicService;
    private readonly IBlogService _blogService;

    public ValidatorHelpers(ITopicService topicService, IBlogService blogService)
    {
        _topicService = topicService;
        _blogService = blogService;
    }
    
    public async Task<bool> IsTopicSystemNameUnique(string systemName,CancellationToken token)
    {
        var topic = await _topicService.GetBySystemName(systemName);
        return topic == null;
    }
    
    public async Task<bool> IsTopicSystemNameUniqueEdit(TopicEditViewViewModel topic,CancellationToken token)
    {
        var topicBySystemName = await _topicService.GetBySystemName(topic.SystemName);
        var topicById = await _topicService.GetById(topic.Id);
        return topicBySystemName == null || topicBySystemName.Id == topicById.Id;
    }    
    public async Task<bool> IsBlogSystemNameUnique(string systemName,CancellationToken token)
    {
        var topic = await _blogService.GetBySystemName(systemName);
        return topic == null;
    }
    
    public async Task<bool> IsBlogSystemNameUniqueEdit(BlogEditViewViewModel blog,CancellationToken token)
    {
        var blogBySystemName = await _blogService.GetBySystemName(blog.SystemName);
        var blogById = await _blogService.GetById(blog.Id);
        return blogBySystemName == null || blogBySystemName.Id == blogById.Id;
    }

    public bool IsUrl(string systemName)
    {
        return Uri.IsWellFormedUriString(systemName,UriKind.RelativeOrAbsolute);
    }
}