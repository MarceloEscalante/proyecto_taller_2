using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sistema_ModParts.Models.DTOs;

namespace Sistema_ModParts.Services
{
    public class MockDashboardService : IDashboardService
    {
        public async Task<DashboardSummaryDto> GetAdminSummaryAsync(CancellationToken token = default)
        {
            await Task.Delay(1000, token); // Simular latencia de red

            return new DashboardSummaryDto
            {
                Kpis = new List<KpiSummaryDto>
                {
                    new KpiSummaryDto { Title = "Ingresos del Mes", Value = "$ 45,230", Trend = "+12%", IsPositiveTrend = true, IconName = "AttachMoney" },
                    new KpiSummaryDto { Title = "Total Pedidos", Value = "124", Trend = "+5%", IsPositiveTrend = true, IconName = "ShoppingCart" },
                    new KpiSummaryDto { Title = "Usuarios Activos", Value = "12", Trend = "0%", IsPositiveTrend = true, IconName = "People" },
                    new KpiSummaryDto { Title = "Alertas de Stock", Value = "5", Trend = "-2", IsPositiveTrend = false, IconName = "Warning" }
                },
                MainChartSeries = new List<ChartSeriesDto>
                {
                    new ChartSeriesDto
                    {
                        SeriesName = "Ventas",
                        Points = new List<ChartPointDto>
                        {
                            new ChartPointDto { Label = "Lun", Value = 1200 },
                            new ChartPointDto { Label = "Mar", Value = 1900 },
                            new ChartPointDto { Label = "Mie", Value = 1500 },
                            new ChartPointDto { Label = "Jue", Value = 2200 },
                            new ChartPointDto { Label = "Vie", Value = 2800 },
                            new ChartPointDto { Label = "Sab", Value = 3100 },
                            new ChartPointDto { Label = "Dom", Value = 900 }
                        }
                    }
                }
            };
        }

        public async Task<DashboardSummaryDto> GetSellerSummaryAsync(CancellationToken token = default)
        {
            await Task.Delay(800, token);

            return new DashboardSummaryDto
            {
                Kpis = new List<KpiSummaryDto>
                {
                    new KpiSummaryDto { Title = "Ventas del Día", Value = "$ 1,200", Trend = "+2%", IsPositiveTrend = true, IconName = "PointOfSale" },
                    new KpiSummaryDto { Title = "Meta Mensual", Value = "85%", Trend = "15% restante", IsPositiveTrend = true, IconName = "TrendingUp" },
                    new KpiSummaryDto { Title = "Ticket Promedio", Value = "$ 85", Trend = "+5%", IsPositiveTrend = true, IconName = "Receipt" },
                    new KpiSummaryDto { Title = "Devoluciones", Value = "2", Trend = "+1", IsPositiveTrend = false, IconName = "AssignmentReturn" }
                },
                MainChartSeries = new List<ChartSeriesDto>()
            };
        }

        public async Task<DashboardSummaryDto> GetWarehouseSummaryAsync(CancellationToken token = default)
        {
            await Task.Delay(800, token);

            return new DashboardSummaryDto
            {
                Kpis = new List<KpiSummaryDto>
                {
                    new KpiSummaryDto { Title = "Bajo Stock", Value = "18", Trend = "+3 hoy", IsPositiveTrend = false, IconName = "Inventory" },
                    new KpiSummaryDto { Title = "Entradas Hoy", Value = "45", Trend = "", IsPositiveTrend = true, IconName = "Input" },
                    new KpiSummaryDto { Title = "Salidas Hoy", Value = "112", Trend = "", IsPositiveTrend = true, IconName = "Output" },
                    new KpiSummaryDto { Title = "Valor Inventario", Value = "$ 1.2M", Trend = "Estable", IsPositiveTrend = true, IconName = "AccountBalance" }
                },
                MainChartSeries = new List<ChartSeriesDto>()
            };
        }
    }
}
