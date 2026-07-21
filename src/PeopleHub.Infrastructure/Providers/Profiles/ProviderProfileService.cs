using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers.Profiles;
using PeopleHub.Contracts.Providers.Profiles;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Infrastructure.Providers.Profiles;

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

        return profile is null
            ? null
            : Map(profile);
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