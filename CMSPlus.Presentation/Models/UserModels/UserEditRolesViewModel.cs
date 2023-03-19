namespace CMSPlus.Domain.Models.UserModels;

public class UserEditRolesViewModel
{
    public string UserId { get; set; }
    public List<UserRole> Roles { get; set; }
}

public class UserRole
{
    public string Name { get; set; }
    public bool IsSelected { get; set; }
}