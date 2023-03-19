using CMSPlus.Domain.Models.AccountModels;
using FluentValidation;

namespace CMSPlus.Presentation.Validations.AccountValidators;

public class RegisterModelValidator:AbstractValidator<RegisterViewModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Password and ConfirmPassword should be equal");
    }
}