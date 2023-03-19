using CMSPlus.Domain.Models.TopicModels;
using CMSPlus.Presentation.Validations.Helpers;
using FluentValidation;

namespace CMSPlus.Presentation.Validations.TopicValidators;

public class TopicCreateModelValidator:AbstractValidator<TopicCreateViewModel>
{
    private readonly ValidatorHelpers _validatorHelpers;
    public TopicCreateModelValidator(ValidatorHelpers validatorHelpers)
    {
        _validatorHelpers = validatorHelpers;
        RuleFor(topic => topic.SystemName)
            .MustAsync(_validatorHelpers.IsTopicSystemNameUnique).WithMessage("System name must be unique");
        RuleFor(topic => topic.SystemName)
            .Must(_validatorHelpers.IsUrl).WithMessage("The system name is not an URL");
    }
}