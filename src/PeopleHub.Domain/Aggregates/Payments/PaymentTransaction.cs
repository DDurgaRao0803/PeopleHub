using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Aggregates.Payments;

public sealed class PaymentTransaction : AuditableEntity
{
    private PaymentTransaction()
    {
    }



    public PaymentTransaction(
        Guid serviceRequestId,
        Guid customerId,
        decimal amount)
    {
        if (serviceRequestId == Guid.Empty)
        {
            throw new ArgumentException(
                "Service request id cannot be empty.");
        }


        if (customerId == Guid.Empty)
        {
            throw new ArgumentException(
                "Customer id cannot be empty.");
        }


        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Payment amount must be greater than zero.");
        }



        ServiceRequestId = serviceRequestId;

        CustomerId = customerId;

        Amount = amount;

        Status = PaymentStatus.Pending;

        CreatedOnUtc = DateTime.UtcNow;
    }



    public Guid ServiceRequestId { get; private set; }



    public Guid CustomerId { get; private set; }



    public decimal Amount { get; private set; }



    public PaymentStatus Status { get; private set; }



    public DateTime? CompletedOnUtc { get; private set; }



    public void Complete()
    {
        if (Status != PaymentStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending payments can be completed.");
        }


        Status = PaymentStatus.Completed;

        CompletedOnUtc = DateTime.UtcNow;
    }



    public void Fail()
    {
        if (Status != PaymentStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending payments can fail.");
        }


        Status = PaymentStatus.Failed;
    }



    public void Refund()
    {
        if (Status != PaymentStatus.Completed)
        {
            throw new InvalidOperationException(
                "Only completed payments can be refunded.");
        }


        Status = PaymentStatus.Refunded;
    }
}