using AutoMapper;
using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Models.TopicModels;

namespace CMSPlus.Presentation.AutoMapperProfiles;

public class TopicProfile:Profile
{
    public TopicProfile()
    {
        CreateMap<TopicEntity,TopicViewViewModel>();
        CreateMap<TopicViewViewModel,TopicEntity>();
        CreateMap<TopicEntity, TopicDetailsViewViewModel>();
        CreateMap<TopicEntity, TopicCreateViewModel>();
        CreateMap<TopicCreateViewModel,TopicEntity>();
        CreateMap<TopicEntity, TopicEditViewViewModel>();
        CreateMap<TopicEditViewViewModel,TopicEntity>();
    }
}