using CMSPlus.Domain.Models.RoleModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMSPlus.Presentation.Validations.RoleValidators;

public class RoleModelsValidator : AbstractValidator<BaseRoleViewModel>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleModelsValidator(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        RuleFor(x => x)
            .MustAsync(IsRoleNameUnique).WithMessage("Name must be not null and unique");
    }

    public async Task<bool> IsRoleNameUnique(BaseRoleViewModel role, CancellationToken token)
    {
        if (role.Name == null)
        {
            return false;
        }

        var roleByName = await _roleManager.FindByNameAsync(role.Name);
        if(roleByName==null)
        {
            return true;
        }

        if (role.Id == null)
        {
            return false;
        }
        var roleById = await _roleManager.FindByIdAsync(roleByName.Id);
        return roleById.Id == role.Id;
    }
}