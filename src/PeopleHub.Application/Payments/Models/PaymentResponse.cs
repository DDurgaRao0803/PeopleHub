namespace PeopleHub.Application.Payments.Models;

public sealed class PaymentResponse
{
    public Guid Id { get; set; }

    public Guid ServiceRequestId { get; set; }

    public Guid CustomerId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? CompletedOnUtc { get; set; }
}