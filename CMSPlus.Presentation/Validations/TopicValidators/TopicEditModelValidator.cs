using CMSPlus.Domain.Models.TopicModels;
using CMSPlus.Presentation.Validations.Helpers;
using FluentValidation;

namespace CMSPlus.Presentation.Validations.TopicValidators;

public class TopicEditModelValidator:AbstractValidator<TopicEditViewViewModel>
{
    private readonly ValidatorHelpers _validatorHelpers;
    public TopicEditModelValidator(ValidatorHelpers validatorHelpers)
    {
        _validatorHelpers = validatorHelpers;
        RuleFor(topic=>topic)
            .MustAsync(_validatorHelpers.IsTopicSystemNameUniqueEdit).WithMessage("System name must be unique");
        RuleFor(topic => topic.SystemName)
            .Must(_validatorHelpers.IsUrl).WithMessage("The system name is not an URL");
    }
}