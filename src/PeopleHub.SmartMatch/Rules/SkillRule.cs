using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.SmartMatch.Rules;

public sealed class SkillRule
{
    public bool IsEligible(
        ProviderProfile provider,
        Guid serviceCategoryId)
    {
        ArgumentNullException.ThrowIfNull(provider);

        return provider.Skills.Any(skill =>
            skill.ServiceCategoryId == serviceCategoryId);
    }
}