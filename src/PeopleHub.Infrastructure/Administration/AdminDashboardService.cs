using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Administration;
using PeopleHub.Contracts.Administration;
using PeopleHub.Domain.Enums;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Administration;

public sealed class AdminDashboardService : IAdminDashboardService
{
    private readonly ApplicationDbContext _context;

    public AdminDashboardService(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardResponse> GetDashboardAsync(
        CancellationToken cancellationToken = default)
    {
        return new DashboardResponse
        {
            TotalUsers = await _context.Users
                .CountAsync(cancellationToken),

            ActiveUsers = await _context.Users
                .CountAsync(
                    x => x.Status == UserStatus.Active,
                    cancellationToken),

            Providers = await _context.ProviderProfiles
                .CountAsync(cancellationToken),

            PendingVerifications = await _context.ProviderVerifications
                .CountAsync(
                    x => x.VerificationStatus == VerificationStatus.Pending,
                    cancellationToken),

            ServiceCategories = await _context.ServiceCategories
                .CountAsync(cancellationToken),

            ServiceRequests = await _context.ServiceRequests
                .CountAsync(cancellationToken),

            CompletedServiceRequests = await _context.ServiceRequests
                .CountAsync(
                    x => x.Status == ServiceRequestStatus.Completed,
                    cancellationToken),

            Reviews = await _context.Reviews
                .CountAsync(cancellationToken)
        };
    }
}