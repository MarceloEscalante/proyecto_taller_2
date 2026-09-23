using System.Collections.Generic;

namespace Sistema_ModParts.Models.DTOs
{
    public class KpiSummaryDto
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Trend { get; set; } = string.Empty;
        public bool IsPositiveTrend { get; set; }
        public string IconName { get; set; } = string.Empty;
    }

    public class ChartSeriesDto
    {
        public string SeriesName { get; set; } = string.Empty;
        public List<ChartPointDto> Points { get; set; } = new();
    }

    public class ChartPointDto
    {
        public string Label { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public class DashboardSummaryDto
    {
        public List<KpiSummaryDto> Kpis { get; set; } = new();
        public List<ChartSeriesDto> MainChartSeries { get; set; } = new();
    }
}
