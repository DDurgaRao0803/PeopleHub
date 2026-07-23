using PeopleHub.Contracts.Providers.Dashboard;

namespace PeopleHub.Application.Providers.Dashboard;

public interface IProviderDashboardService
{
    Task<ProviderDashboardResponse> GetDashboardAsync(
        CancellationToken cancellationToken = default);
}