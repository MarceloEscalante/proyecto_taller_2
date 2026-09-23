using System;
using System.Collections.Generic;
using Sistema_ModParts.ViewModels;

namespace Sistema_ModParts.Navigation;

public sealed class NavigationRegistry
{
    private readonly Dictionary<string, NavigationRoute> routes = new(StringComparer.OrdinalIgnoreCase);

    public NavigationRegistry()
    {
        Register(new NavigationRoute(
            Key: "demo.overview",
            Title: "Overview",
            Breadcrumb: ["Demo", "Overview"],
            SidebarGroup: "Workspace",
            Description: "A demonstration destination for the navigation infrastructure.",
            ViewModelFactory: () => new DemoOverviewViewModel()));

        Register(new NavigationRoute(
            Key: "demo.preview",
            Title: "Preview",
            Breadcrumb: ["Demo", "Preview"],
            SidebarGroup: "Workspace",
            Description: "A second destination used to verify content replacement.",
            ViewModelFactory: () => new DemoPreviewViewModel()));

        Register(new NavigationRoute(
            Key: "dashboard.admin",
            Title: "Administrador",
            Breadcrumb: ["Dashboards", "Administrador"],
            SidebarGroup: "Dashboards",
            Description: "Vista global de la operación y sus indicadores futuros.",
            ViewModelFactory: () => 
            {
                var svc = new Services.MockDashboardService();
                return new AdminDashboardViewModel(svc);
            }));

        Register(new NavigationRoute(
            Key: "dashboard.seller",
            Title: "Vendedor",
            Breadcrumb: ["Dashboards", "Vendedor"],
            SidebarGroup: "Dashboards",
            Description: "Vista de la actividad comercial y sus indicadores futuros.",
            ViewModelFactory: () => 
            {
                var svc = new Services.MockDashboardService();
                return new SellerDashboardViewModel(svc);
            }));

        Register(new NavigationRoute(
            Key: "dashboard.warehouse",
            Title: "Depósito",
            Breadcrumb: ["Dashboards", "Depósito"],
            SidebarGroup: "Dashboards",
            Description: "Vista del control de inventario y sus indicadores futuros.",
            ViewModelFactory: () => 
            {
                var svc = new Services.MockDashboardService();
                return new WarehouseDashboardViewModel(svc);
            }));

        Register(new NavigationRoute(
            Key: "module.products",
            Title: "Productos",
            Breadcrumb: ["Catálogo", "Productos"],
            SidebarGroup: "Catálogo",
            Description: "Área reservada para el catálogo de productos.",
            ViewModelFactory: () => 
            {
                var context = new Models.AppDbContext();
                var prodRepo = new Repositories.ProductoRepository(context);
                var catRepo = new Repositories.CategoriaRepository(context);
                var prodService = new Services.ProductoService(prodRepo);
                return new ProductosViewModel(prodService, catRepo);
            }));

        Register(new NavigationRoute(
            Key: "module.categories",
            Title: "Categorías",
            Breadcrumb: ["Catálogo", "Productos", "Categorías"],
            SidebarGroup: "Catálogo",
            Description: "Área reservada para la clasificación del catálogo.",
            ViewModelFactory: () => 
            {
                var context = new Models.AppDbContext();
                var catRepo = new Repositories.CategoriaRepository(context);
                var catService = new Services.CategoriaService(catRepo);
                return new CategoriasViewModel(catService);
            }));

        Register(new NavigationRoute(
            Key: "module.customers",
            Title: "Clientes",
            Breadcrumb: ["Operaciones", "Clientes"],
            SidebarGroup: "Operaciones",
            Description: "Área reservada para la gestión visual de clientes.",
            ViewModelFactory: () => new ModulePlaceholderViewModel(
                "Clientes",
                "Área reservada para la gestión visual de clientes.",
                "module.customers",
                "Operaciones / Clientes",
                "operations",
                ModulePlaceholderKind.Standard)));

        Register(new NavigationRoute(
            Key: "module.sales",
            Title: "Ventas",
            Breadcrumb: ["Operaciones", "Ventas"],
            SidebarGroup: "Operaciones",
            Description: "Área de Punto de Venta (Mostrador).",
            ViewModelFactory: () => 
            {
                var context = new Models.AppDbContext();
                var operationsService = new Services.Operations.OperationsService(context);
                return new ViewModels.Operations.PosViewModel(operationsService);
            }));

        Register(new NavigationRoute(
            Key: "module.inventory",
            Title: "Inventario",
            Breadcrumb: ["Operaciones", "Inventario"],
            SidebarGroup: "Operaciones",
            Description: "Área reservada para el control visual de inventario.",
            ViewModelFactory: () => new ModulePlaceholderViewModel(
                "Inventario",
                "Área reservada para el control visual de inventario.",
                "module.inventory",
                "Operaciones / Inventario",
                "operations",
                ModulePlaceholderKind.Standard)));

        Register(new NavigationRoute(
            Key: "module.users",
            Title: "Usuarios",
            Breadcrumb: ["Administración", "Usuarios"],
            SidebarGroup: "Administración",
            Description: "Gestión visual de los usuarios y sus roles.",
            ViewModelFactory: () => 
            {
                var context = new Models.AppDbContext();
                var empRepo = new Repositories.EmpleadoRepository(context);
                var rolRepo = new Repositories.RolRepository(context);
                var empService = new Services.EmpleadoService(empRepo);
                return new EmpleadosViewModel(empService, rolRepo);
            }));

        Register(new NavigationRoute(
            Key: "module.reports",
            Title: "Reportes",
            Breadcrumb: ["Administración", "Reportes"],
            SidebarGroup: "Administración",
            Description: "Área reservada para consultas y reportes futuros.",
            ViewModelFactory: () => new ModulePlaceholderViewModel(
                "Reportes",
                "Área reservada para consultas y reportes futuros.",
                "module.reports",
                "Administración / Reportes",
                "reporting",
                ModulePlaceholderKind.Grid)));
    }

    public IReadOnlyCollection<NavigationRoute> Routes => routes.Values;

    public void Register(NavigationRoute route)
    {
        ArgumentNullException.ThrowIfNull(route);

        if (string.IsNullOrWhiteSpace(route.Key))
        {
            throw new ArgumentException("A navigation route must have a key.", nameof(route));
        }

        if (string.IsNullOrWhiteSpace(route.Title))
        {
            throw new ArgumentException("A navigation route must have a title.", nameof(route));
        }

        if (route.Breadcrumb.Count == 0)
        {
            throw new ArgumentException("A navigation route must have at least one breadcrumb segment.", nameof(route));
        }

        if (!routes.TryAdd(route.Key, route))
        {
            throw new InvalidOperationException($"The navigation route '{route.Key}' is already registered.");
        }
    }

    public bool TryGet(string key, out NavigationRoute? route)
    {
        return routes.TryGetValue(key, out route);
    }
}
