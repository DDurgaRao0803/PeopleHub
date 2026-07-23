namespace PeopleHub.Contracts.Providers.ServiceRequests;

public sealed record ServiceRequestResponse(
    Guid Id,
    Guid CustomerId,
    Guid? ProviderProfileId,
    Guid ServiceCategoryId,
    string Title,
    string Description,
    DateTime RequestedDate,
    string Status);