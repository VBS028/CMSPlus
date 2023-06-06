using CMSPlus.Domain.Models.AccountModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMSPlus.Presentation.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    //todo fix redirect to prev page.
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IValidator<RegisterViewModel> _registerModelValidator;
    private readonly IValidator<LoginViewModel> _loginModelValidator;
    private readonly IEmailSender _emailSender;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IValidator<RegisterViewModel> registerModelValidator,
        IValidator<LoginViewModel> loginModelValidator,
        IEmailSender emailSender,
        RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _registerModelValidator = registerModelValidator;
        _loginModelValidator = loginModelValidator;
        _emailSender = emailSender;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, string prevPath = null)
    {
        var validationResult = await _registerModelValidator.ValidateAsync(model);
        if (validationResult.IsValid)
        {
            var user = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email
            };

            var createResult = await _userManager.CreateAsync(user, model.Password);
            if (createResult.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                if (await _roleManager.FindByNameAsync("User") is null)
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("SuccessfulRegistration");
            }

            AddErrors(createResult);
        }

        validationResult.AddToModelState(this.ModelState);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var validationResult = await _loginModelValidator.ValidateAsync(model);
        if (validationResult.IsValid)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                model.RememberMe, lockoutOnFailure: false);
            if (loginResult.Succeeded)
            {
                return Redirect("/");
            }

            if (loginResult.IsNotAllowed)
            {
                ModelState.AddModelError(String.Empty, "Please confirm your email");
                return View(model);
            }

            ModelState.AddModelError(String.Empty, "Invalid login attempt");
            return View(model);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmationLink()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmationLink(ConfirmationLinkViewModel model)
    {
        if (model == null)
        {
            throw new ArgumentException();
        }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return RedirectToAction("SuccessfulRegistration");
        }

        if (user.EmailConfirmed)
        {
            ModelState.AddModelError(String.Empty, "This email are already confirmed.");
            return RedirectToAction("SuccessfulRegistration");
        }

        var messageTemplate = @"
<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"">
  <title>Email Confirmation</title>
  <style>
    /* CSS styles here */
  </style>
</head>
<body>
  <div class=""container"">
    <h2>Email Confirmation</h2>
    <p>Dear [Recipient Name],</p>
    <p>Thank you for registering. Please click the link below to confirm your email:</p>
    <p><a href=""[Confirmation Link]"" class=""confirmation-link"">Confirm Email</a></p>
    <p>If you did not register on our website, please ignore this email.</p>
  </div>
</body>
</html>";
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
            protocol: HttpContext.Request.Scheme);
        messageTemplate = messageTemplate.Replace("[Recipient Name]", user.UserName)
            .Replace("[Confirmation Link]", callbackUrl);
        await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
            messageTemplate);
        return RedirectToAction("SuccessfulRegistration");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userid, string code)
    {
        if (userid == null || code == null)
        {
            throw new ArgumentException();
        }

        var user = await _userManager.FindByIdAsync(userid);
        if (user == null)
        {
            throw new ArgumentException();
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        return View(result.Succeeded ? "ConfirmEmail" : "Error");
    }

    [HttpGet]
    public async Task<IActionResult> SuccessfulRegistration()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !user.EmailConfirmed)
        {
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code },
            protocol: HttpContext.Request.Scheme);
        await _emailSender.SendEmailAsync(model.Email, "Reset Password",
            "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
        return View("ForgotPasswordConfirmation");
    }

    [HttpGet]
    public async Task<IActionResult> ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string code = null)
    {
        return code == null ? RedirectToAction("Index", "Home") : View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        var validationResult = await _registerModelValidator.ValidateAsync(model);
        if (validationResult.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            AddErrors(result);
        }

        validationResult.AddToModelState(this.ModelState);
        return View(model);
    }

    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}