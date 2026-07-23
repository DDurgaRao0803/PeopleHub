using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers.Bidding;
using PeopleHub.Contracts.Providers.Bidding;
using PeopleHub.Domain.Aggregates.Bidding;

namespace PeopleHub.Infrastructure.Providers.Bidding;

public sealed class ProviderBidService
    : IProviderBidService
{
    private readonly IProviderBidRepository _providerBidRepository;
    private readonly IServiceRequestRepository _serviceRequestRepository;
    private readonly IUnitOfWork _unitOfWork;


    public ProviderBidService(
        IProviderBidRepository providerBidRepository,
        IServiceRequestRepository serviceRequestRepository,
        IUnitOfWork unitOfWork)
    {
        _providerBidRepository = providerBidRepository;
        _serviceRequestRepository = serviceRequestRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<ProviderBidResponse> SubmitBidAsync(
        Guid providerProfileId,
        CreateProviderBidRequest request,
        CancellationToken cancellationToken = default)
    {
        var serviceRequest =
            await _serviceRequestRepository.GetByIdAsync(
                request.ServiceRequestId,
                cancellationToken);


        if (serviceRequest is null)
        {
            throw new KeyNotFoundException(
                "Service request not found.");
        }


        var bid = new ProviderBid(
            request.ServiceRequestId,
            providerProfileId,
            request.Amount,
            request.EstimatedMinutes);


        await _providerBidRepository.AddAsync(
            bid,
            cancellationToken);


        await _unitOfWork.SaveChangesAsync(
            cancellationToken);


        return MapToResponse(bid);
    }



    public async Task<IReadOnlyList<ProviderBidResponse>> GetBidsAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default)
    {
        var bids =
            await _providerBidRepository
                .GetByServiceRequestIdAsync(
                    serviceRequestId,
                    cancellationToken);


        return bids
            .Select(MapToResponse)
            .ToList();
    }



    public async Task<ProviderBidResponse> AcceptBidAsync(
        Guid bidId,
        CancellationToken cancellationToken = default)
    {
        var bid =
            await _providerBidRepository.GetByIdAsync(
                bidId,
                cancellationToken);


        if (bid is null)
        {
            throw new KeyNotFoundException(
                "Provider bid not found.");
        }


        bid.Accept();


        await _providerBidRepository.UpdateAsync(
            bid,
            cancellationToken);


        await _unitOfWork.SaveChangesAsync(
            cancellationToken);


        return MapToResponse(bid);
    }



    private static ProviderBidResponse MapToResponse(
        ProviderBid bid)
    {
        return new ProviderBidResponse(
            bid.Id,
            bid.ServiceRequestId,
            bid.ProviderProfileId,
            bid.Amount,
            bid.EstimatedMinutes,
            bid.Status.ToString());
    }
}