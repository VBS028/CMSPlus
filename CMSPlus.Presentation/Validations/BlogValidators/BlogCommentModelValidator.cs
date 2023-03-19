using CMSPlus.Domain.Models.TopicModels;
using FluentValidation;

namespace CMSPlus.Presentation.Validations.BlogValidators;

public class BlogCommentModelValidator:AbstractValidator<BlogCommentCreateViewModel>
{
    public BlogCommentModelValidator()
    {
        RuleFor(x => x.Body).NotEmpty();
    }
}