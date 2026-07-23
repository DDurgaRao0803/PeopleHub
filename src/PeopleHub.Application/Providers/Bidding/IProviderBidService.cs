using PeopleHub.Contracts.Providers.Bidding;

namespace PeopleHub.Application.Providers.Bidding;

public interface IProviderBidService
{
    Task<ProviderBidResponse> SubmitBidAsync(
        Guid providerProfileId,
        CreateProviderBidRequest request,
        CancellationToken cancellationToken = default);


    Task<IReadOnlyList<ProviderBidResponse>> GetBidsAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default);


    Task<ProviderBidResponse> AcceptBidAsync(
        Guid bidId,
        CancellationToken cancellationToken = default);
}