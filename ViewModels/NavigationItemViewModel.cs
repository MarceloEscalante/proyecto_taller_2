using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sistema_ModParts.Navigation;

namespace Sistema_ModParts.ViewModels;

public partial class NavigationItemViewModel : ViewModelBase
{
    public NavigationItemViewModel(NavigationRoute route, Action<string> navigate)
    {
        Route = route ?? throw new ArgumentNullException(nameof(route));
        ArgumentNullException.ThrowIfNull(navigate);
        NavigateCommand = new RelayCommand(() => navigate(Route.Key));
    }

    public NavigationRoute Route { get; }

    public string Title => Route.Title;

    public string Group => Route.SidebarGroup ?? string.Empty;

    public RelayCommand NavigateCommand { get; }

    [ObservableProperty]
    private bool isActive;

}
