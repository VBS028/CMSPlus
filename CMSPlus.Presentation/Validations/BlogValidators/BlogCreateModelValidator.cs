using CMSPlus.Domain.Models.TopicModels;
using CMSPlus.Presentation.Validations.Helpers;
using FluentValidation;

namespace CMSPlus.Presentation.Validations.BlogValidators;

public class BlogCreateModelValidator:AbstractValidator<BlogCreateViewModel>
{
    private readonly ValidatorHelpers _validatorHelpers;
    public BlogCreateModelValidator(ValidatorHelpers validatorHelpers)
    {
        _validatorHelpers = validatorHelpers;
        RuleFor(blog => blog.SystemName)
            .MustAsync(_validatorHelpers.IsBlogSystemNameUnique).WithMessage("System name must be unique");
        RuleFor(blog => blog.SystemName)
            .Must(_validatorHelpers.IsUrl).WithMessage("The system name is not an URL");
        RuleFor(blog => blog.Body).NotEmpty();
        RuleFor(blog => blog.SystemName).NotEmpty();
    }
}