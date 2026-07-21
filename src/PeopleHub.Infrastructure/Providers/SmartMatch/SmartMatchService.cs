using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.SmartMatch.Interfaces;
using PeopleHub.Application.SmartMatch.Models;
using PeopleHub.SmartMatch.Interfaces;

namespace PeopleHub.Infrastructure.Providers.SmartMatch;

public sealed class SmartMatchService : ISmartMatchService
{
    private readonly IServiceRequestRepository _serviceRequestRepository;
    private readonly IProviderRepository _providerRepository;
    private readonly ISmartMatchEngine _smartMatchEngine;

    public SmartMatchService(
        IServiceRequestRepository serviceRequestRepository,
        IProviderRepository providerRepository,
        ISmartMatchEngine smartMatchEngine)
    {
        _serviceRequestRepository = serviceRequestRepository;
        _providerRepository = providerRepository;
        _smartMatchEngine = smartMatchEngine;
    }

    public async Task<SmartMatchResponse> FindBestProviderAsync(
    Guid serviceRequestId,
    CancellationToken cancellationToken = default)
{
    var serviceRequest = await _serviceRequestRepository.GetByIdAsync(
        serviceRequestId,
        cancellationToken);

    if (serviceRequest is null)
    {
        throw new KeyNotFoundException("Service request not found.");
    }

    var providers = await _providerRepository.GetEligibleProvidersAsync(
    serviceRequest.ServiceCategoryId,
    cancellationToken);

    var matchResult = _smartMatchEngine.FindBestMatch(
    serviceRequest,
    providers);

    return new SmartMatchResponse
{
    SelectedProviderId = matchResult.SelectedProvider?.Id,
    CandidateCount = matchResult.Scores.Count
};
}
}