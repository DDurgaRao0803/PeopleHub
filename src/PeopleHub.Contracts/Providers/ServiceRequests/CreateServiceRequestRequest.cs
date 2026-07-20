namespace PeopleHub.Contracts.Providers.ServiceRequests;

public sealed record CreateServiceRequestRequest(
    Guid ProviderProfileId,
    Guid ServiceCategoryId,
    string Title,
    string Description,
    DateTime RequestedDate);