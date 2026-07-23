using PeopleHub.Application.Payments.Models;

namespace PeopleHub.Application.Payments;

public interface IWalletService
{
    Task<WalletResponse> GetWalletAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);



    Task<WalletResponse> CreditAsync(
        Guid providerProfileId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default);



    Task<WalletResponse> DebitAsync(
        Guid providerProfileId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default);
}