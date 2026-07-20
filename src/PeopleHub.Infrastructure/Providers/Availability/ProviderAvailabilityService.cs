using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers.Availability;
using PeopleHub.Contracts.Providers.Availability;

namespace PeopleHub.Infrastructure.Providers.Availability;

public sealed class ProviderAvailabilityService : IProviderAvailabilityService
{
    private readonly IProviderProfileRepository _providerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProviderAvailabilityService(
        IProviderProfileRepository providerRepository,
        IUnitOfWork unitOfWork)
    {
        _providerRepository = providerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ProviderAvailabilityResponse>> GetAvailabilityAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        var profile = await _providerRepository.GetByIdWithAvailabilityAsync(
            providerProfileId,
            cancellationToken)
            ?? throw new KeyNotFoundException(
                "Provider profile not found.");

        return profile.Availabilities
            .OrderBy(a => a.DayOfWeek)
            .ThenBy(a => a.StartTime)
            .Select(a => new ProviderAvailabilityResponse(
                a.Id,
                a.DayOfWeek,
                a.StartTime,
                a.EndTime))
            .ToList();
    }

    public async Task<ProviderAvailabilityResponse> AddAvailabilityAsync(
        Guid providerProfileId,
        CreateProviderAvailabilityRequest request,
        CancellationToken cancellationToken = default)
    {
        var profile = await _providerRepository.GetByIdWithAvailabilityAsync(
            providerProfileId,
            cancellationToken)
            ?? throw new KeyNotFoundException(
                "Provider profile not found.");

        var availability = profile.AddAvailability(
            request.DayOfWeek,
            request.StartTime,
            request.EndTime);

        if (availability is null)
        {
            throw new InvalidOperationException(
                "Failed to create availability.");
        }

        await _providerRepository.AddAvailabilityAsync(
            availability,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProviderAvailabilityResponse(
            availability.Id,
            availability.DayOfWeek,
            availability.StartTime,
            availability.EndTime);
    }

    public async Task<ProviderAvailabilityResponse> UpdateAvailabilityAsync(
        Guid providerProfileId,
        Guid availabilityId,
        UpdateProviderAvailabilityRequest request,
        CancellationToken cancellationToken = default)
    {
        var profile = await _providerRepository.GetByIdWithAvailabilityAsync(
            providerProfileId,
            cancellationToken)
            ?? throw new KeyNotFoundException(
                "Provider profile not found.");

        profile.UpdateAvailability(
            availabilityId,
            request.DayOfWeek,
            request.StartTime,
            request.EndTime);

        await _providerRepository.UpdateAsync(
            profile,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var availability = profile.GetAvailability(availabilityId)
            ?? throw new KeyNotFoundException(
                "Availability not found.");

        return new ProviderAvailabilityResponse(
            availability.Id,
            availability.DayOfWeek,
            availability.StartTime,
            availability.EndTime);
    }

    public async Task DeleteAvailabilityAsync(
        Guid providerProfileId,
        Guid availabilityId,
        CancellationToken cancellationToken = default)
    {
        var profile = await _providerRepository.GetByIdWithAvailabilityAsync(
            providerProfileId,
            cancellationToken)
            ?? throw new KeyNotFoundException(
                "Provider profile not found.");

        profile.RemoveAvailability(availabilityId);

        await _providerRepository.UpdateAsync(
            profile,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
    }
}