using PeopleHub.Domain.Aggregates.Payments;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IPaymentTransactionRepository
{
    Task AddAsync(
        PaymentTransaction payment,
        CancellationToken cancellationToken = default);


    Task<PaymentTransaction?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);


    Task UpdateAsync(
        PaymentTransaction payment,
        CancellationToken cancellationToken = default);
}