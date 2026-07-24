public sealed record CreateServiceRequestRequest(
    Guid ServiceCategoryId,
    string Title,
    string Description,
    DateTime RequestedDate);