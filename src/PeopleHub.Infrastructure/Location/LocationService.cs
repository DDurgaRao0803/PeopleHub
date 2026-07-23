using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Realtime;
using PeopleHub.Application.Location;
using PeopleHub.Contracts.Location;
using PeopleHub.Domain.Aggregates.Location;

namespace PeopleHub.Infrastructure.Location;

public sealed class LocationService
    : ILocationService
{
    private readonly IProviderLocationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRealtimeNotifier _realtimeNotifier;



    public LocationService(
        IProviderLocationRepository repository,
        IUnitOfWork unitOfWork,
        IRealtimeNotifier realtimeNotifier)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _realtimeNotifier = realtimeNotifier;
    }



    public async Task<ProviderLocationResponse> UpdateLocationAsync(
        Guid providerProfileId,
        UpdateLocationRequest request,
        CancellationToken cancellationToken = default)
    {
        var location =
            await _repository
                .GetByProviderProfileIdAsync(
                    providerProfileId,
                    cancellationToken);



        if (location is null)
        {
            location =
                new ProviderLocation(
                    providerProfileId,
                    request.Latitude,
                    request.Longitude);


            await _repository.AddAsync(
                location,
                cancellationToken);
        }
        else
        {
            location.UpdateLocation(
                request.Latitude,
                request.Longitude);


            await _repository.UpdateAsync(
                location,
                cancellationToken);
        }



        await _unitOfWork.SaveChangesAsync(
            cancellationToken);



        await _realtimeNotifier.SendLocationUpdateAsync(
            providerProfileId,
            request.Latitude,
            request.Longitude,
            DateTime.UtcNow,
            cancellationToken);



        return Map(location);
    }



    public async Task<ProviderLocationResponse?> GetProviderLocationAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        var location =
            await _repository
                .GetByProviderProfileIdAsync(
                    providerProfileId,
                    cancellationToken);



        return location is null
            ? null
            : Map(location);
    }



    private static ProviderLocationResponse Map(
        ProviderLocation location)
    {
        return new ProviderLocationResponse
        {
            ProviderProfileId = location.ProviderProfileId,

            Latitude = location.Latitude,

            Longitude = location.Longitude,

            UpdatedOnUtc = location.UpdatedOnUtc
        };
    }
}