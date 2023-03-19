using System.Security.Claims;
using CMSPlus.Domain.Models.RoleModels;
using CMSPlus.Presentation;
using CMSPlus.Presentation.Pages.Account;
using CMSPlus.Presentation.Pages.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;

namespace CMSPlus.Presentation.Controllers;

[Authorize(Roles="Admin")]
public class RoleController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IValidator<BaseRoleViewModel> _validator;

    public RoleController(
        UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        IValidator<BaseRoleViewModel> validator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _validator = validator;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var rolesViewModel = new List<RoleGetViewModel>();
        if (!roles.IsNullOrEmpty())
        {
            foreach (var role in roles)
            {
                var members = await _userManager.GetUsersInRoleAsync(role.Name);
                rolesViewModel.Add(new RoleGetViewModel()
                {
                    Id=role.Id,
                    Name=role.Name,
                    Members=members.Select(x=>x.UserName)
                });
            }
        }

        return View(rolesViewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateViewModel role)
    {
        var validationResult = await _validator.ValidateAsync(role);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View();
        }
        var newRole = new IdentityRole(role.Name);
        await _roleManager.CreateAsync(newRole);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            throw new ArgumentException($"Role with id: {id} wasn't found!");
        }

        var permissions = Permissions.GetAllPermissions();
        var claims = await _roleManager.GetClaimsAsync(role);
        var claimsValues = claims.Where(x=>x.Type=="Permission").Select(x => x.Value);
        var rolePermissions = permissions.Select(x => new RolePermission()
        {
            Name = x,
            IsSelected = claimsValues.Contains(x)
        }).ToList();
        var model = new RoleEditViewModel()
        {
            Id=id,
            Name=role.Name,
            Permissions = rolePermissions
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RoleEditViewModel model)
    {
        var role = await _roleManager.FindByIdAsync(model.Id);
        
        if (role != null)
        {
            var validationResult = await _validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return await Edit(model.Id);
            }
            role.Name = model.Name;
            await _roleManager.UpdateAsync(role);
            foreach (var permission in model.Permissions)
            {
                var claim = new Claim("Permission", permission.Name);
                await _roleManager.AddClaimAsync(role, claim);
            }
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedPermissions = model.Permissions.Where(a => a.IsSelected).ToList();
            foreach (var claim in selectedPermissions)
            {
                await _roleManager.AddClaimAsync(role, new Claim("Permission", claim.Name));
            }
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        throw new ArgumentException();
    }
}