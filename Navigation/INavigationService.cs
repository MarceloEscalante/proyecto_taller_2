namespace Sistema_ModParts.Navigation;

public interface INavigationService
{
    NavigationState State { get; }

    void Navigate(string routeKey);
}
