using System.Diagnostics;
using CMSPlus.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSPlus.Presentation.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public async Task<IActionResult>Index()
    {
        return View();
    }

    public async Task<IActionResult>Privacy()
    {
        return View();
    }
}