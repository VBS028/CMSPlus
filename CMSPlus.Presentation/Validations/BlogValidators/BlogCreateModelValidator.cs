using CMSPlus.Presentation.Models.BlogModels;
using CMSPlus.Presentation.Validations.Helpers;
using FluentValidation;

namespace CMSPlus.Presentation.Validations.BlogValidators;

public class BlogCreateModelValidator:AbstractValidator<BlogCreateViewModel>
{
    private readonly ValidatorHelpers _validatorHelpers;
    public BlogCreateModelValidator(ValidatorHelpers validatorHelpers)
    {
        _validatorHelpers = validatorHelpers;
        RuleFor(blog => blog.Body).NotEmpty();
        RuleFor(blog => blog.SystemName).NotEmpty();
    }
}