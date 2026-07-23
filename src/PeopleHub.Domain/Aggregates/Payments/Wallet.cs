using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Aggregates.Payments;

public sealed class Wallet : AuditableEntity
{
    private Wallet()
    {
    }



    public Wallet(
        Guid providerProfileId)
    {
        if (providerProfileId == Guid.Empty)
        {
            throw new ArgumentException(
                "Provider profile id cannot be empty.");
        }


        ProviderProfileId = providerProfileId;

        Balance = 0m;
    }



    public Guid ProviderProfileId { get; private set; }



    public decimal Balance { get; private set; }



    public void Credit(
        decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Credit amount must be greater than zero.");
        }


        Balance += amount;
    }



    public void Debit(
        decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Debit amount must be greater than zero.");
        }


        if (Balance < amount)
        {
            throw new InvalidOperationException(
                "Insufficient wallet balance.");
        }


        Balance -= amount;
    }
}