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
        CreateMap<BlogCommentEntity, BlogCommentViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.BlogId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
            .ForMember(dest => dest.CreatedOnUtc, opt => opt.MapFrom(src => src.CreatedOnUtc))
            .ForMember(dest => dest.UpdatedOnUtc, opt => opt.MapFrom(src => src.UpdatedOnUtc))
            .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies))
            .ReverseMap();
    }
}