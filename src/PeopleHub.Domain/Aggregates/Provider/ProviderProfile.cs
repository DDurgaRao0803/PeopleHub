using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Aggregates.Provider;

public class ProviderProfile : AuditableEntity
{
    private readonly List<ProviderSkill> _skills = [];

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

public void RemoveSkill(Guid serviceCategoryId)
{
    var skill = _skills.FirstOrDefault(s => s.ServiceCategoryId == serviceCategoryId);

    if (skill is null)
    {
        return;
    }

    _skills.Remove(skill);
}
}