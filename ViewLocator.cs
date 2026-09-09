using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Sistema_ModParts.Components.Shell;
using Sistema_ModParts.ViewModels;
using Sistema_ModParts.Views.Dashboards;
using Sistema_ModParts.Views.Modules;

namespace Sistema_ModParts;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        if (param is ShellViewModel)
        {
            return new ShellView();
        }

        if (param is AdminDashboardViewModel)
        {
            return new AdminDashboardView();
        }

        if (param is SellerDashboardViewModel)
        {
            return new SellerDashboardView();
        }

        if (param is WarehouseDashboardViewModel)
        {
            return new WarehouseDashboardView();
        }

        if (param is ModulePlaceholderViewModel)
        {
            return new ModulePlaceholderView();
        }

        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
