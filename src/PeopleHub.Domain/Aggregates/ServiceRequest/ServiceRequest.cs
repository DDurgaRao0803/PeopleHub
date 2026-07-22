using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Entities;

public class ServiceRequest : AuditableEntity
{
    private ServiceRequest()
    {
    }

    public ServiceRequest(
        Guid customerId,
        Guid providerProfileId,
        Guid serviceCategoryId,
        string title,
        string description,
        DateTime requestedDate)
    {
        CustomerId = customerId;
        ProviderProfileId = providerProfileId;
        ServiceCategoryId = serviceCategoryId;
        Title = title;
        Description = description;
        RequestedDate = requestedDate;
        Status = ServiceRequestStatus.Pending;
    }

    public Guid CustomerId { get; private set; }

    public Guid ProviderProfileId { get; private set; }

    public Guid ServiceCategoryId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public DateTime RequestedDate { get; private set; }

    public ServiceRequestStatus Status { get; private set; }

    public void Accept()
    {
        if (Status != ServiceRequestStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending requests can be accepted.");
        }

        Status = ServiceRequestStatus.Accepted;
    }

    public void Complete()
    {
        if (Status != ServiceRequestStatus.Accepted)
        {
            throw new InvalidOperationException(
                "Only accepted requests can be completed.");
        }

        Status = ServiceRequestStatus.Completed;
    }

    public void Reject()
{
    if (Status != ServiceRequestStatus.Pending)
    {
        throw new InvalidOperationException(
            "Only pending requests can be rejected.");
    }

    Status = ServiceRequestStatus.Rejected;
}

public void Cancel()
{
    if (Status == ServiceRequestStatus.Completed)
    {
        throw new InvalidOperationException(
            "Completed requests cannot be cancelled.");
    }

    Status = ServiceRequestStatus.Cancelled;
}

}