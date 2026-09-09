using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Sistema_ModParts.Navigation;

namespace Sistema_ModParts.ViewModels;

public partial class ShellViewModel : ViewModelBase
{
    private readonly INavigationService navigationService;

    public ShellViewModel(INavigationService navigationService, NavigationRegistry registry)
    {
        this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        ArgumentNullException.ThrowIfNull(registry);

        NavigationItems = new ObservableCollection<NavigationItemViewModel>();
        WorkspaceItems = new ObservableCollection<NavigationItemViewModel>();
        DashboardItems = new ObservableCollection<NavigationItemViewModel>();
        CatalogItems = new ObservableCollection<NavigationItemViewModel>();
        OperationsItems = new ObservableCollection<NavigationItemViewModel>();
        AdministrationItems = new ObservableCollection<NavigationItemViewModel>();

        foreach (var route in registry.Routes)
        {
            var item = new NavigationItemViewModel(route, Navigate);
            NavigationItems.Add(item);

            switch (route.SidebarGroup)
            {
                case "Dashboards":
                    DashboardItems.Add(item);
                    break;
                case "Catálogo":
                    CatalogItems.Add(item);
                    break;
                case "Operaciones":
                    OperationsItems.Add(item);
                    break;
                case "Administración":
                    AdministrationItems.Add(item);
                    break;
                default:
                    WorkspaceItems.Add(item);
                    break;
            }
        }

        navigationService.State.PropertyChanged += OnNavigationStateChanged;

        if (NavigationItems.Count > 0)
        {
            Navigate(NavigationItems[0].Route.Key);
        }
    }

    public ObservableCollection<NavigationItemViewModel> NavigationItems { get; }

    public ObservableCollection<NavigationItemViewModel> WorkspaceItems { get; }

    public ObservableCollection<NavigationItemViewModel> DashboardItems { get; }

    public ObservableCollection<NavigationItemViewModel> CatalogItems { get; }

    public ObservableCollection<NavigationItemViewModel> OperationsItems { get; }

    public ObservableCollection<NavigationItemViewModel> AdministrationItems { get; }

    public object? CurrentViewModel => navigationService.State.CurrentViewModel;

    public string CurrentTitle => navigationService.State.CurrentRoute?.Title ?? "Workspace";

    public string CurrentDescription => navigationService.State.CurrentRoute?.Description ?? string.Empty;

    public string CurrentBreadcrumb => navigationService.State.CurrentRoute?.BreadcrumbText ?? string.Empty;

    private void Navigate(string routeKey)
    {
        navigationService.Navigate(routeKey);
    }

    private void OnNavigationStateChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(NavigationState.CurrentRoute) or nameof(NavigationState.CurrentViewModel))
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(CurrentTitle));
            OnPropertyChanged(nameof(CurrentDescription));
            OnPropertyChanged(nameof(CurrentBreadcrumb));

            foreach (var item in NavigationItems)
            {
                item.IsActive = string.Equals(
                    item.Route.Key,
                    navigationService.State.CurrentRoute?.Key,
                    StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
