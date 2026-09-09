namespace Sistema_ModParts.ViewModels;

public sealed class SellerDashboardViewModel : DashboardViewModelBase
{
    public SellerDashboardViewModel()
        : base(
            title: "Dashboard Vendedor",
            description: "Vista de la actividad comercial y sus indicadores futuros.",
            variantId: "seller")
    {
    }
}
