using PeopleHub.Application.Payments;
using PeopleHub.Application.Payments.Models;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Payments;
using PeopleHub.Domain.Enums;


namespace PeopleHub.Infrastructure.Payments;

public sealed class WalletService
    : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletTransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;


    public WalletService(
        IWalletRepository walletRepository,
        IWalletTransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }



    public async Task<WalletResponse> GetWalletAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        var wallet =
            await _walletRepository
                .GetByProviderProfileIdAsync(
                    providerProfileId,
                    cancellationToken);


        if (wallet is null)
{
    throw new InvalidOperationException(
        "Wallet does not exist.");
}


        return Map(wallet);
    }



    public async Task<WalletResponse> CreditAsync(
        Guid providerProfileId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default)
    {
        var wallet =
            await _walletRepository
                .GetByProviderProfileIdAsync(
                    providerProfileId,
                    cancellationToken);



        if (wallet is null)
        {
            wallet =
                new Wallet(providerProfileId);


            await _walletRepository
                .AddAsync(
                    wallet,
                    cancellationToken);


            await _unitOfWork
                .SaveChangesAsync(
                    cancellationToken);
        }



        wallet.Credit(amount);



        var transaction =
            new WalletTransaction(
                wallet.Id,
                amount,
                WalletTransactionType.Earning,
                description);



        await _transactionRepository
            .AddAsync(
                transaction,
                cancellationToken);



        await _walletRepository
            .UpdateAsync(
                wallet,
                cancellationToken);



        await _unitOfWork
            .SaveChangesAsync(
                cancellationToken);



        return Map(wallet);
    }



    public async Task<WalletResponse> DebitAsync(
        Guid providerProfileId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default)
    {
        var wallet =
            await _walletRepository
                .GetByProviderProfileIdAsync(
                    providerProfileId,
                    cancellationToken);



        if (wallet is null)
        {
            throw new InvalidOperationException(
                "Wallet does not exist.");
        }



        wallet.Debit(amount);



        var transaction =
            new WalletTransaction(
                wallet.Id,
                amount,
                WalletTransactionType.Withdrawal,
                description);



        await _transactionRepository
            .AddAsync(
                transaction,
                cancellationToken);



        await _walletRepository
            .UpdateAsync(
                wallet,
                cancellationToken);



        await _unitOfWork
            .SaveChangesAsync(
                cancellationToken);



        return Map(wallet);
    }



    private static WalletResponse Map(
        Wallet wallet)
    {
        return new WalletResponse
        {
            ProviderProfileId =
                wallet.ProviderProfileId,

            Balance =
                wallet.Balance
        };
    }
}