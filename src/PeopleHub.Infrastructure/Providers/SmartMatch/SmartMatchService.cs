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
    private readonly IUnitOfWork _unitOfWork;

    public SmartMatchService(
        IServiceRequestRepository serviceRequestRepository,
        IProviderRepository providerRepository,
        ISmartMatchEngine smartMatchEngine,
        IUnitOfWork unitOfWork)
    {
        _serviceRequestRepository = serviceRequestRepository;
        _providerRepository = providerRepository;
        _smartMatchEngine = smartMatchEngine;
        _unitOfWork = unitOfWork;
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
            throw new KeyNotFoundException(
                "Service request not found.");
        }

        var providers = await _providerRepository.GetEligibleProvidersAsync(
            serviceRequest.ServiceCategoryId,
            cancellationToken);

        var matchResult = _smartMatchEngine.FindBestMatch(
            serviceRequest,
            providers);


        if (matchResult.SelectedProvider is not null)
        {
            serviceRequest.AssignProvider(
                matchResult.SelectedProvider.Id);

            await _serviceRequestRepository.UpdateAsync(
                serviceRequest,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }


        return new SmartMatchResponse
        {
            SelectedProviderId = matchResult.SelectedProvider?.Id,
            CandidateCount = matchResult.Scores.Count
        };
    }
}