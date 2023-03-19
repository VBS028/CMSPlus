using CMSPlus.Domain.Models.AccountModels;
using FluentValidation;

namespace CMSPlus.Presentation.Validations.AccountValidators;

public class LoginModelValidator:AbstractValidator<LoginViewModel>
{
    public LoginModelValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}