namespace Sistema_ModParts.ViewModels;

public enum ModulePlaceholderKind
{
    Standard,
    Submodule,
    Grid,
}

public sealed class ModulePlaceholderViewModel : ViewModelBase
{
    public ModulePlaceholderViewModel(
        string title,
        string description,
        string routeKey,
        string breadcrumb,
        string moduleType,
        ModulePlaceholderKind placeholderKind)
    {
        Title = title;
        Description = description;
        RouteKey = routeKey;
        Breadcrumb = breadcrumb;
        ModuleType = moduleType;
        PlaceholderKind = placeholderKind;
    }

    public string Title { get; }

    public string Description { get; }

    public string RouteKey { get; }

    public string Breadcrumb { get; }

    public string ModuleType { get; }

    public ModulePlaceholderKind PlaceholderKind { get; }

    public bool ShowDataGrid => PlaceholderKind == ModulePlaceholderKind.Grid;

    public bool ShowEmptyState => !ShowDataGrid;
}
