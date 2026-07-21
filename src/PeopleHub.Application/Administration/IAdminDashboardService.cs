using PeopleHub.Contracts.Administration;

namespace PeopleHub.Application.Administration;

public interface IAdminDashboardService
{
    Task<DashboardResponse> GetDashboardAsync(
        CancellationToken cancellationToken = default);
}