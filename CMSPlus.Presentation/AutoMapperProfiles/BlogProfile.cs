using AutoMapper;
using CMSPlus.Domain.Dtos;
using CMSPlus.Domain.Entities;
using CMSPlus.Presentation.Models.BlogModels;

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
        CreateMap<BlogCommentDto, BlogEntity>().ReverseMap();
        CreateMap<BlogCommentWithAttachmentDto, BlogEntity>().ReverseMap();        
        CreateMap<BlogCommentDto, BlogCreateViewModel>().ReverseMap();
        CreateMap<BlogCommentWithAttachmentDto, BlogCreateViewModel>().ReverseMap();
        CreateMap<BlogCommentWithAttachmentDto,BlogCommentDto>().ReverseMap();  
    }
}