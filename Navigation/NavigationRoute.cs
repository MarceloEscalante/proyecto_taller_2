using System;
using System.Collections.Generic;

namespace Sistema_ModParts.Navigation;

public sealed record NavigationRoute(
    string Key,
    string Title,
    IReadOnlyList<string> Breadcrumb,
    string? SidebarGroup = null,
    string? Description = null,
    Func<object>? ViewModelFactory = null)
{
    public string BreadcrumbText => string.Join(" / ", Breadcrumb);
}
