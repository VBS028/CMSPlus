using System.ComponentModel.DataAnnotations;

namespace CMSPlus.Domain.Models.AccountModels;

public class RegisterViewModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}