using System.Threading;
using System.Threading.Tasks;
using Sistema_ModParts.Models.DTOs;

namespace Sistema_ModParts.Services
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetAdminSummaryAsync(CancellationToken token = default);
        Task<DashboardSummaryDto> GetSellerSummaryAsync(CancellationToken token = default);
        Task<DashboardSummaryDto> GetWarehouseSummaryAsync(CancellationToken token = default);
    }
}
