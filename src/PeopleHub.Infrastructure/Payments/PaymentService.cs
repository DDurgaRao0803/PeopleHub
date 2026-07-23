using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Payments;
using PeopleHub.Application.Payments.Models;
using PeopleHub.Domain.Aggregates.Payments;

namespace PeopleHub.Infrastructure.Payments;

public sealed class PaymentService
    : IPaymentService
{
    private readonly IPaymentTransactionRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;


    public PaymentService(
        IPaymentTransactionRepository paymentRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }



    public async Task<PaymentResponse> CreatePaymentAsync(
        Guid serviceRequestId,
        Guid customerId,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        var payment =
            new PaymentTransaction(
                serviceRequestId,
                customerId,
                amount);


        await _paymentRepository.AddAsync(
            payment,
            cancellationToken);


        await _unitOfWork.SaveChangesAsync(
            cancellationToken);


        return Map(payment);
    }



    public async Task<PaymentResponse> CompletePaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default)
    {
        var payment =
            await GetPaymentAsync(
                paymentId,
                cancellationToken);


        payment.Complete();


        await _paymentRepository.UpdateAsync(
            payment,
            cancellationToken);


        await _unitOfWork.SaveChangesAsync(
            cancellationToken);


        return Map(payment);
    }



    public async Task<PaymentResponse> FailPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default)
    {
        var payment =
            await GetPaymentAsync(
                paymentId,
                cancellationToken);


        payment.Fail();


        await _paymentRepository.UpdateAsync(
            payment,
            cancellationToken);


        await _unitOfWork.SaveChangesAsync(
            cancellationToken);


        return Map(payment);
    }



    public async Task<PaymentResponse> RefundPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default)
    {
        var payment =
            await GetPaymentAsync(
                paymentId,
                cancellationToken);


        payment.Refund();


        await _paymentRepository.UpdateAsync(
            payment,
            cancellationToken);


        await _unitOfWork.SaveChangesAsync(
            cancellationToken);


        return Map(payment);
    }



    private async Task<PaymentTransaction> GetPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        var payment =
            await _paymentRepository.GetByIdAsync(
                paymentId,
                cancellationToken);


        if (payment is null)
        {
            throw new KeyNotFoundException(
                "Payment not found.");
        }


        return payment;
    }



    private static PaymentResponse Map(
        PaymentTransaction payment)
    {
        return new PaymentResponse
        {
            Id = payment.Id,

            ServiceRequestId =
                payment.ServiceRequestId,

            CustomerId =
                payment.CustomerId,

            Amount =
                payment.Amount,

            Status =
                payment.Status.ToString(),

            CreatedOnUtc =
                payment.CreatedOnUtc,

            CompletedOnUtc =
                payment.CompletedOnUtc
        };
    }
}