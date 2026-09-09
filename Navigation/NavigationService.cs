using System;
using System.Collections.Generic;

namespace Sistema_ModParts.Navigation;

public sealed class NavigationService : INavigationService
{
    private readonly NavigationRegistry registry;

    public NavigationService(NavigationRegistry registry, NavigationState state)
    {
        this.registry = registry ?? throw new ArgumentNullException(nameof(registry));
        State = state ?? throw new ArgumentNullException(nameof(state));
    }

    public NavigationState State { get; }

    public void Navigate(string routeKey)
    {
        if (string.IsNullOrWhiteSpace(routeKey))
        {
            throw new ArgumentException("A route key is required.", nameof(routeKey));
        }

        if (!registry.TryGet(routeKey, out var route) || route is null)
        {
            throw new KeyNotFoundException($"The navigation route '{routeKey}' is not registered.");
        }

        State.SetCurrent(route, route.ViewModelFactory?.Invoke());
    }
}
