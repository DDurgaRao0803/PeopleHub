using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Enums;

namespace PeopleHub.SmartMatch.Rules;

public sealed class VerificationRule
{
    public bool IsEligible(ProviderProfile provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        return provider.VerificationStatus == VerificationStatus.Approved;
    }
}