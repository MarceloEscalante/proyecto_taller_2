namespace Sistema_ModParts.ViewModels;

public sealed class AdminDashboardViewModel : DashboardViewModelBase
{
    public AdminDashboardViewModel()
        : base(
            title: "Dashboard Administrador",
            description: "Vista global de la operación y sus indicadores futuros.",
            variantId: "admin")
    {
    }
}
