using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Aggregates.Provider;

public sealed class ProviderSkill : Entity
{
    private ProviderSkill()
    {
        ServiceCategory = null!;
    }

    public ProviderSkill(Guid providerProfileId, ServiceCategory serviceCategory)
    {
        ArgumentNullException.ThrowIfNull(serviceCategory);

        ProviderProfileId = providerProfileId;
        ServiceCategory = serviceCategory;
        ServiceCategoryId = serviceCategory.Id;
    }

    public Guid ProviderProfileId { get; private set; }

    public Guid ServiceCategoryId { get; private set; }

    public ServiceCategory ServiceCategory { get; private set; }
}