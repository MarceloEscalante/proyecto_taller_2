using CommunityToolkit.Mvvm.ComponentModel;
using Sistema_ModParts.Navigation;

namespace Sistema_ModParts.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ShellViewModel shellViewModel;

    public MainWindowViewModel()
    {
        var registry = new NavigationRegistry();
        var state = new NavigationState();
        var navigationService = new NavigationService(registry, state);

        shellViewModel = new ShellViewModel(navigationService, registry);
        CurrentPresentationViewModel = new LoginViewModel(ShowShell);
    }

    [ObservableProperty]
    private ViewModelBase currentPresentationViewModel;

    private void ShowShell()
    {
        CurrentPresentationViewModel = shellViewModel;
    }
}
