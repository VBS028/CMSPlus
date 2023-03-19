using Microsoft.AspNetCore.Authorization;

namespace CMSPlus.Presentation.CustomPolicy.CustomRequirements;

public class PermissionRequirement:IAuthorizationRequirement
{
    public string Permission { get; private set; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}