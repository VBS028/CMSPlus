using CMSPlus.Domain.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSPlus.Presentation.Controllers;

[Authorize(Roles="Admin")]
public class UserController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    // GET
    public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.Select(x => new UserViewModel()
        {
            Id = x.Id,
            Email = x.Email
        }).ToListAsync();
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> EditUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var roles = await _userManager.GetRolesAsync(user);

        var userRoles = await _roleManager.Roles.Select(x => new UserRole()
        {
            Name = x.Name,
            IsSelected = roles.Contains(x.Name)
        }).ToListAsync();
        ViewData["Username"] = user.UserName;
        return View(new UserEditRolesViewModel()
        {
            UserId = userId,
            Roles = userRoles
        });
    }

    [HttpPost]
    public async Task<IActionResult> EditUserRoles(UserEditRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        var newRoles = model.Roles.Where(x => x.IsSelected).Select(x => x.Name);
        await _userManager.AddToRolesAsync(user, newRoles);
        
        return RedirectToAction("Index");
    }
}