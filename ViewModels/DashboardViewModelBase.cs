using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sistema_ModParts.Models.DTOs;
using Sistema_ModParts.Services;

namespace Sistema_ModParts.ViewModels;

public abstract partial class DashboardViewModelBase : ViewModelBase
{
    protected readonly IDashboardService _dashboardService;

    protected DashboardViewModelBase(string title, string description, string variantId, IDashboardService dashboardService)
    {
        Title = title;
        Description = description;
        VariantId = variantId;
        _dashboardService = dashboardService;
    }

    public string Title { get; }
    public string Description { get; }
    public string VariantId { get; }

    [ObservableProperty]
    private bool _isLoading = true;

    [ObservableProperty]
    private bool _hasError;

    [ObservableProperty]
    private ObservableCollection<KpiSummaryDto> _kpis = new();

    [ObservableProperty]
    private ObservableCollection<ChartSeriesDto> _mainChartSeries = new();

    [RelayCommand]
    public abstract Task LoadDataAsync();
}
