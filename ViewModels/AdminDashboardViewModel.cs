using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Sistema_ModParts.Services;

namespace Sistema_ModParts.ViewModels;

public sealed partial class AdminDashboardViewModel : DashboardViewModelBase
{
    public AdminDashboardViewModel(IDashboardService dashboardService)
        : base(
            title: "Dashboard Administrador",
            description: "Vista global de la operación y sus indicadores futuros.",
            variantId: "admin",
            dashboardService)
    {
        _ = LoadDataAsync();
    }

    public override async Task LoadDataAsync()
    {
        IsLoading = true;
        HasError = false;

        try
        {
            var summary = await _dashboardService.GetAdminSummaryAsync();
            Kpis = new ObservableCollection<Models.DTOs.KpiSummaryDto>(summary.Kpis);
            MainChartSeries = new ObservableCollection<Models.DTOs.ChartSeriesDto>(summary.MainChartSeries);
        }
        catch (Exception)
        {
            HasError = true;
        }
        finally
        {
            IsLoading = false;
        }
    }
}
