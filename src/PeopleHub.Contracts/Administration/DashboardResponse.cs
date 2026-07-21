namespace PeopleHub.Contracts.Administration;

public sealed class DashboardResponse
{
    public int TotalUsers { get; init; }

    public int ActiveUsers { get; init; }

    public int Providers { get; init; }

    public int PendingVerifications { get; init; }

    public int ServiceCategories { get; init; }

    public int ServiceRequests { get; init; }

    public int CompletedServiceRequests { get; init; }

    public int Reviews { get; init; }
}