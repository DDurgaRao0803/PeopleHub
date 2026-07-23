using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Payments;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Payments;

public sealed class PaymentTransactionRepository
    : IPaymentTransactionRepository
{
    private readonly ApplicationDbContext _context;


    public PaymentTransactionRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task AddAsync(
        PaymentTransaction payment,
        CancellationToken cancellationToken = default)
    {
        await _context.PaymentTransactions
            .AddAsync(
                payment,
                cancellationToken);
    }



    public async Task<PaymentTransaction?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.PaymentTransactions
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }



    public Task UpdateAsync(
        PaymentTransaction payment,
        CancellationToken cancellationToken = default)
    {
        _context.PaymentTransactions.Update(payment);

        return Task.CompletedTask;
    }
}