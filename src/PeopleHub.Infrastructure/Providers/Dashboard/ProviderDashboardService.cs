using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Application.Providers.Dashboard;
using PeopleHub.Contracts.Providers.Dashboard;

namespace PeopleHub.Infrastructure.Providers.Dashboard;

public sealed class ProviderDashboardService : IProviderDashboardService
{
    private readonly IProviderRepository _providerRepository;
    private readonly IServiceRequestRepository _serviceRequestRepository;
    private readonly ICurrentUserService _currentUserService;

    public ProviderDashboardService(
        IProviderRepository providerRepository,
        IServiceRequestRepository serviceRequestRepository,
        ICurrentUserService currentUserService)
    {
        _providerRepository = providerRepository;
        _serviceRequestRepository = serviceRequestRepository;
        _currentUserService = currentUserService;
    }


    public async Task<ProviderDashboardResponse> GetDashboardAsync(
        CancellationToken cancellationToken = default)
    {
        var provider = await _providerRepository.GetByUserIdAsync(
            _currentUserService.UserId,
            cancellationToken);

        if (provider is null)
        {
            throw new KeyNotFoundException(
                "Provider profile not found.");
        }


        var requests = await _serviceRequestRepository
            .GetByProviderProfileIdAsync(
                provider.Id,
                cancellationToken);


        return new ProviderDashboardResponse(
            provider.Id,
            provider.VerificationStatus.ToString(),
            provider.AverageRating,
            provider.CompletedJobs,
            provider.ResponseRate,
            provider.LastActiveUtc,
            requests.Count(x => x.Status == Domain.Enums.ServiceRequestStatus.Pending),
            requests.Count(x => x.Status == Domain.Enums.ServiceRequestStatus.Accepted),
            requests.Count(x => x.Status == Domain.Enums.ServiceRequestStatus.Completed),
            requests.Count(x => x.Status == Domain.Enums.ServiceRequestStatus.Cancelled));
    }
}