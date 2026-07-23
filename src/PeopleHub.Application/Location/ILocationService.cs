using PeopleHub.Contracts.Location;

namespace PeopleHub.Application.Location;

public interface ILocationService
{
    Task<ProviderLocationResponse> UpdateLocationAsync(
        Guid providerProfileId,
        UpdateLocationRequest request,
        CancellationToken cancellationToken = default);


    Task<ProviderLocationResponse?> GetProviderLocationAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);
}