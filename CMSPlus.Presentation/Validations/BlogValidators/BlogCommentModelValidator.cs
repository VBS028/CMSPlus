using CMSPlus.Presentation.Models.BlogModels;
using FluentValidation;

namespace CMSPlus.Presentation.Validations.BlogValidators;

public class BlogCommentModelValidator:AbstractValidator<BlogCommentCreateViewModel>
{
    public BlogCommentModelValidator()
    {
        RuleFor(x => x.Body).NotEmpty();
    }
}