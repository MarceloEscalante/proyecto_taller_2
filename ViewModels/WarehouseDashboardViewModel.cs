namespace Sistema_ModParts.ViewModels;

public sealed class WarehouseDashboardViewModel : DashboardViewModelBase
{
    public WarehouseDashboardViewModel()
        : base(
            title: "Dashboard Depósito",
            description: "Vista del control de inventario y sus indicadores futuros.",
            variantId: "warehouse")
    {
    }
}
