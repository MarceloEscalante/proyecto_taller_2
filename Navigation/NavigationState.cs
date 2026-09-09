using CommunityToolkit.Mvvm.ComponentModel;

namespace Sistema_ModParts.Navigation;

public partial class NavigationState : ObservableObject
{
    [ObservableProperty]
    private NavigationRoute? currentRoute;

    [ObservableProperty]
    private object? currentViewModel;

    public bool HasCurrentRoute => CurrentRoute is not null;

    internal void SetCurrent(NavigationRoute route, object? viewModel)
    {
        CurrentRoute = route;
        CurrentViewModel = viewModel;
        OnPropertyChanged(nameof(HasCurrentRoute));
    }
}
