using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Sistema_ModParts.Services;

namespace Sistema_ModParts.ViewModels;

public sealed partial class WarehouseDashboardViewModel : DashboardViewModelBase
{
    public WarehouseDashboardViewModel(IDashboardService dashboardService)
        : base(
            title: "Dashboard Depósito",
            description: "Vista del control de inventario y sus indicadores futuros.",
            variantId: "warehouse",
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
            var summary = await _dashboardService.GetWarehouseSummaryAsync();
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
