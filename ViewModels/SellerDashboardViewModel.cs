using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Sistema_ModParts.Services;

namespace Sistema_ModParts.ViewModels;

public sealed partial class SellerDashboardViewModel : DashboardViewModelBase
{
    public SellerDashboardViewModel(IDashboardService dashboardService)
        : base(
            title: "Dashboard Vendedor",
            description: "Vista de la actividad comercial y sus indicadores futuros.",
            variantId: "seller",
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
            var summary = await _dashboardService.GetSellerSummaryAsync();
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
