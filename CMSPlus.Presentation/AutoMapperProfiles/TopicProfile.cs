using AutoMapper;
using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Models.TopicModels;

namespace CMSPlus.Presentation.AutoMapperProfiles;

public class TopicProfile:Profile
{
    public TopicProfile()
    {
        CreateMap<TopicEntity,TopicViewViewModel>().ReverseMap();
        CreateMap<TopicViewViewModel,TopicEntity>().ReverseMap();
        CreateMap<TopicEntity, TopicDetailsViewViewModel>().ReverseMap();
        CreateMap<TopicEntity, TopicCreateViewModel>().ReverseMap();
        CreateMap<TopicCreateViewModel,TopicEntity>().ReverseMap();
        CreateMap<TopicEntity, TopicEditViewViewModel>().ReverseMap();
        CreateMap<TopicEditViewViewModel,TopicEntity>().ReverseMap();
    }
}