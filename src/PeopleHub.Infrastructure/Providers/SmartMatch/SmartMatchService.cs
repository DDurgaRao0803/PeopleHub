using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.SmartMatch.Interfaces;
using PeopleHub.Application.SmartMatch.Models;
using PeopleHub.Domain.Enums;
using PeopleHub.SmartMatch.Interfaces;
using PeopleHub.SmartMatch.Models;
using PeopleHub.Application.Notifications;
using PeopleHub.Contracts.Notifications;

namespace PeopleHub.Infrastructure.Providers.SmartMatch;

public sealed class SmartMatchService : ISmartMatchService
{
    private readonly IServiceRequestRepository _serviceRequestRepository;
    private readonly IProviderRepository _providerRepository;
    private readonly IProviderBidRepository _providerBidRepository;
    private readonly ISmartMatchEngine _smartMatchEngine;
    private readonly IUnitOfWork _unitOfWork;

    private readonly INotificationService _notificationService;


    public SmartMatchService(
    IServiceRequestRepository serviceRequestRepository,
    IProviderRepository providerRepository,
    IProviderBidRepository providerBidRepository,
    ISmartMatchEngine smartMatchEngine,
    IUnitOfWork unitOfWork,
    INotificationService notificationService)
{
    _serviceRequestRepository = serviceRequestRepository;
    _providerRepository = providerRepository;
    _providerBidRepository = providerBidRepository;
    _smartMatchEngine = smartMatchEngine;
    _unitOfWork = unitOfWork;
    _notificationService = notificationService;
}



    public async Task<SmartMatchResponse> FindBestProviderAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default)
    {
        var serviceRequest =
            await _serviceRequestRepository.GetByIdAsync(
                serviceRequestId,
                cancellationToken);


        if (serviceRequest is null)
        {
            throw new KeyNotFoundException(
                "Service request not found.");
        }



        var bids =
            await _providerBidRepository
                .GetByServiceRequestIdAsync(
                    serviceRequestId,
                    cancellationToken);



        var submittedBids =
            bids
                .Where(x =>
                    x.Status == ProviderBidStatus.Submitted)
                .ToList();



        if (submittedBids.Count == 0)
        {
            throw new InvalidOperationException(
                "No provider bids available.");
        }



        var providers =
            await _providerRepository
                .GetEligibleProvidersAsync(
                    serviceRequest.ServiceCategoryId,
                    cancellationToken);



        var candidates =
            submittedBids
                .Join(
                    providers,
                    bid => bid.ProviderProfileId,
                    provider => provider.Id,
                    (bid, provider) =>
                        new SmartMatchCandidate
                        {
                            Provider = provider,
                            Bid = bid
                        })
                .ToList();



        var matchResult =
            _smartMatchEngine.FindBestMatch(
                serviceRequest,
                candidates);



        if (matchResult.SelectedProvider is null)
        {
            throw new InvalidOperationException(
                "No suitable provider found.");
        }



        var selectedProviderId =
            matchResult.SelectedProvider.Id;



        serviceRequest.AssignProvider(
            selectedProviderId);



        var winningBid =
            submittedBids.First(
                x => x.ProviderProfileId == selectedProviderId);



        winningBid.Accept();

        await _notificationService.CreateAsync(
    serviceRequest.CustomerId,
    new CreateNotificationRequest
    {
        Type = (int)NotificationType.ProviderSelected,

        Title = "Provider Selected",

        Message = "A provider has been selected for your service request."
    },
    cancellationToken);



await _notificationService.CreateAsync(
    matchResult.SelectedProvider.UserId,
    new CreateNotificationRequest
    {
        Type = (int)NotificationType.ProviderSelected,

        Title = "New Service Assigned",

        Message = "You have been selected for a service request."
    },
    cancellationToken);



        foreach (var bid in submittedBids)
        {
            if (bid.Id != winningBid.Id)
            {
                bid.Reject();

                await _providerBidRepository.UpdateAsync(
                    bid,
                    cancellationToken);
            }
        }



        await _providerBidRepository.UpdateAsync(
            winningBid,
            cancellationToken);



        await _serviceRequestRepository.UpdateAsync(
            serviceRequest,
            cancellationToken);



        await _unitOfWork.SaveChangesAsync(
            cancellationToken);



        return new SmartMatchResponse
        {
            SelectedProviderId = selectedProviderId,
            CandidateCount = candidates.Count
        };
    }
}