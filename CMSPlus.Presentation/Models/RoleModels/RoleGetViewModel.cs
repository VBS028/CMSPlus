using Microsoft.AspNetCore.Identity;

namespace CMSPlus.Domain.Models.RoleModels;

public class RoleGetViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string>? Members { get; set; }
}