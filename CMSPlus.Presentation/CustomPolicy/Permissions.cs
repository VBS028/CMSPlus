using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CMSPlus.Presentation;

public static class Permissions
{
    public static IEnumerable<string> GetPermissionTypes()
    {
        return typeof(Permissions).GetNestedTypes().Select(x => x.Name);
    }
    public static IEnumerable<string> GetPermissions(string typeName)
    { 
        typeName = $"{typeof(Permissions)?.ToString()}+{typeName}";
        return Type.GetType(typeName).GetFields().Select(x=>x.GetValue(null).ToString());
    }

    public static IEnumerable<string> GetAllPermissions()
    {
        var permissionTypes = GetPermissionTypes();
        return permissionTypes.SelectMany(GetPermissions);
    }
    public static class Blog
    {
        public const string View = "Permissions.Blog.View";
        public const string Create = "Permissions.Blog.Create";
        public const string Edit = "Permissions.Blog.Edit";
        public const string Delete = "Permissions.Blog.Delete";
    }

    public class Topic
    {
        public const string View = "Permissions.Topic.View";
        public const string Create = "Permissions.Topic.Create";
        public const string Edit = "Permissions.Topic.Edit";
        public const string Delete = "Permissions.Topic.Delete";
        public const string GetEmail = "Permission.Topic.GetEmail";
    }
}
