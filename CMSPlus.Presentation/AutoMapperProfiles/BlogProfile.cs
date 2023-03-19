using AutoMapper;
using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Models.TopicModels;

namespace CMSPlus.Presentation.AutoMapperProfiles;

public class BlogProfile:Profile
{
    public BlogProfile()
    {
        CreateMap<BlogEntity, BlogGetViewViewModel>().ReverseMap();
        CreateMap<BlogCommentEntity, BlogCommentViewModel>().ReverseMap();
        CreateMap<BlogCreateViewModel, BlogEntity>().ReverseMap();
        CreateMap<BlogEditViewViewModel, BlogEntity>().ReverseMap();
        CreateMap<BlogCommentCreateViewModel, BlogCommentEntity>().ReverseMap();
    }
}