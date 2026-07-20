using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Aggregates.Provider;

public class ProviderProfile : AuditableEntity
{
    private readonly List<ProviderSkill> _skills = [];

    private readonly List<ProviderAvailability> _availabilities = [];

    private ProviderProfile()
    {
    }

    public ProviderProfile(
        Guid userId,
        string bio,
        int experienceYears)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(experienceYears);
        ArgumentException.ThrowIfNullOrWhiteSpace(bio);

        UserId = userId;
        Bio = bio.Trim();
        ExperienceYears = experienceYears;
        VerificationStatus = VerificationStatus.NotSubmitted;
    }

    public Guid UserId { get; private set; }

    public string Bio { get; private set; } = null!;

    public int ExperienceYears { get; private set; }

    public VerificationStatus VerificationStatus { get; private set; }

    public IReadOnlyCollection<ProviderSkill> Skills => _skills;

    public IReadOnlyCollection<ProviderAvailability> Availabilities =>
    _availabilities.AsReadOnly();

    public void UpdateBio(string bio)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(bio);

        Bio = bio.Trim();
    }

    public void UpdateExperience(int experienceYears)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(experienceYears);

        ExperienceYears = experienceYears;
    }

    public void Approve()
{
    VerificationStatus = VerificationStatus.Approved;
}

    public void Reject()
    {
        VerificationStatus = VerificationStatus.Rejected;
    }

    public void AddSkill(ServiceCategory serviceCategory)
{
    ArgumentNullException.ThrowIfNull(serviceCategory);

    if (_skills.Any(skill => skill.ServiceCategoryId == serviceCategory.Id))
    {
        return;
    }

    _skills.Add(new ProviderSkill(Id, serviceCategory));
}

public ProviderAvailability AddAvailability(
    DayOfWeek dayOfWeek,
    TimeOnly startTime,
    TimeOnly endTime)
{
    if (startTime >= endTime)
    {
        throw new ArgumentException(
            "Start time must be earlier than end time.");
    }

    var overlaps = _availabilities.Any(a =>
        a.DayOfWeek == dayOfWeek &&
        startTime < a.EndTime &&
        endTime > a.StartTime);

    if (overlaps)
    {
        throw new InvalidOperationException(
            "Availability overlaps with an existing time slot.");
    }

    var availability = new ProviderAvailability(
        Id,
        dayOfWeek,
        startTime,
        endTime);

    _availabilities.Add(availability);

    return availability;
}

public void RemoveAvailability(Guid availabilityId)
{
    var availability = _availabilities.FirstOrDefault(a => a.Id == availabilityId);

    if (availability is null)
    {
        return;
    }

    _availabilities.Remove(availability);
}

public ProviderAvailability? GetAvailability(Guid availabilityId)
{
    return _availabilities.FirstOrDefault(a => a.Id == availabilityId);
}


public void RemoveSkill(Guid serviceCategoryId)
{
    var skill = _skills.FirstOrDefault(s => s.ServiceCategoryId == serviceCategoryId);

    if (skill is null)
    {
        return;
    }

    _skills.Remove(skill);
}

public void UpdateAvailability(
    Guid availabilityId,
    DayOfWeek dayOfWeek,
    TimeOnly startTime,
    TimeOnly endTime)
{
    var availability = _availabilities.FirstOrDefault(a => a.Id == availabilityId);

    if (availability is null)
    {
        throw new InvalidOperationException("Availability not found.");
    }

    var overlaps = _availabilities.Any(a =>
        a.Id != availabilityId &&
        a.DayOfWeek == dayOfWeek &&
        startTime < a.EndTime &&
        endTime > a.StartTime);

    if (overlaps)
    {
        throw new InvalidOperationException(
            "Availability overlaps with an existing time slot.");
    }

    availability.Update(
        dayOfWeek,
        startTime,
        endTime);
}

}