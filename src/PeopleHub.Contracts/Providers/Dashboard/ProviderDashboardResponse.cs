namespace PeopleHub.Contracts.Providers.Dashboard;

public sealed record ProviderDashboardResponse(
    Guid ProviderProfileId,
    string VerificationStatus,
    decimal AverageRating,
    int CompletedJobs,
    decimal ResponseRate,
    DateTime LastActiveUtc,
    int PendingRequests,
    int AcceptedRequests,
    int CompletedRequests,
    int CancelledRequests);