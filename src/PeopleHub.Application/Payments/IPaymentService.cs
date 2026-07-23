using PeopleHub.Application.Payments.Models;

namespace PeopleHub.Application.Payments;

public interface IPaymentService
{
    Task<PaymentResponse> CreatePaymentAsync(
        Guid serviceRequestId,
        Guid customerId,
        decimal amount,
        CancellationToken cancellationToken = default);



    Task<PaymentResponse> CompletePaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);



    Task<PaymentResponse> FailPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);



    Task<PaymentResponse> RefundPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);
}