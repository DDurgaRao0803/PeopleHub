namespace PeopleHub.Application.Payments.Models;

public sealed class WalletResponse
{
    public Guid Id { get; set; }

    public Guid ProviderProfileId { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedOnUtc { get; set; }
}