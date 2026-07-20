using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers;
using PeopleHub.Contracts.Providers;
using PeopleHub.Contracts.Providers.Availability;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Infrastructure.Providers;

public sealed class ProviderProfileService : IProviderProfileService
{
    private readonly IProviderProfileRepository _providerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProviderProfileService(
        IProviderProfileRepository providerRepository,
        IUnitOfWork unitOfWork)
    {
        _providerRepository = providerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProviderProfileResponse> CreateAsync(
        Guid userId,
        CreateProviderProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        var existing = await _providerRepository.GetByUserIdAsync(
            userId,
            cancellationToken);

        if (existing is not null)
        {
            throw new InvalidOperationException(
                "Provider profile already exists.");
        }

        var profile = new ProviderProfile(
            userId,
            request.Bio,
            request.ExperienceYears);

        await _providerRepository.AddAsync(
            profile,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Map(profile);
    }

    public async Task<ProviderProfileResponse?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var profile = await _providerRepository.GetByUserIdAsync(
            userId,
            cancellationToken);

        return profile is null ? null : Map(profile);
    }

    public async Task<ProviderProfileResponse> UpdateAsync(
        Guid userId,
        UpdateProviderProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        var profile = await _providerRepository.GetByUserIdAsync(
            userId,
            cancellationToken)
            ?? throw new KeyNotFoundException(
                "Provider profile not found.");

        profile.UpdateBio(request.Bio);
        profile.UpdateExperience(request.ExperienceYears);

        await _providerRepository.UpdateAsync(
            profile,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Map(profile);
    }

    public async Task DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var profile = await _providerRepository.GetByUserIdAsync(
            userId,
            cancellationToken)
            ?? throw new KeyNotFoundException(
                "Provider profile not found.");

        await _providerRepository.DeleteAsync(
            profile,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProviderAvailabilityResponse> AddAvailabilityAsync(
    Guid providerProfileId,
    CreateProviderAvailabilityRequest request,
    CancellationToken cancellationToken = default)
{
    var profile = await _providerRepository.GetByIdWithAvailabilityAsync(
        providerProfileId,
        cancellationToken)
        ?? throw new KeyNotFoundException("Provider profile not found.");

    var availability = profile.AddAvailability(
        request.DayOfWeek,
        request.StartTime,
        request.EndTime);

    if (availability is null)
    {
        throw new InvalidOperationException("Failed to create availability.");
    }

    // Explicitly add the new availability to the context
    // so EF Core recognizes it for INSERT instead of UPDATE
    await _providerRepository.AddAvailabilityAsync(availability, cancellationToken);

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

    await _unitOfWork.SaveChangesAsync(cancellationToken);
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

    private static ProviderProfileResponse Map(
        ProviderProfile profile)
    {
        return new ProviderProfileResponse
        {
            Id = profile.Id,
            UserId = profile.UserId,
            Bio = profile.Bio,
            ExperienceYears = profile.ExperienceYears,
            VerificationStatus = profile.VerificationStatus.ToString()
        };
    }
}