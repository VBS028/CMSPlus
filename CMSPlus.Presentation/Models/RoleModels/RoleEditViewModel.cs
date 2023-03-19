using Azure.Core;

namespace CMSPlus.Domain.Models.RoleModels;

public class RoleEditViewModel:BaseRoleViewModel
{
    public List<RolePermission> Permissions { get; set; }
}

public class RolePermission
{
    public string Name { get; set; }
    public bool IsSelected { get; set; }
}