using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Aggregates.Payments;

public sealed class WalletTransaction : AuditableEntity
{
    private WalletTransaction()
    {
    }



    public WalletTransaction(
        Guid walletId,
        decimal amount,
        WalletTransactionType type,
        string description)
    {
        if (walletId == Guid.Empty)
        {
            throw new ArgumentException(
                "Wallet id cannot be empty.");
        }


        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Amount must be greater than zero.");
        }


        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException(
                "Description is required.");
        }



        WalletId = walletId;

        Amount = amount;

        Type = type;

        Description = description;
    }



    public Guid WalletId { get; private set; }



    public decimal Amount { get; private set; }



    public WalletTransactionType Type { get; private set; }



    public string Description { get; private set; } = string.Empty;
}