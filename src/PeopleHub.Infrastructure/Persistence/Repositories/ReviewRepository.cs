using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Review;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Review review,
        CancellationToken cancellationToken = default)
    {
        await _context.Reviews.AddAsync(review, cancellationToken);
    }

    public async Task<Review?> GetByIdAsync(
        Guid reviewId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(
                x => x.Id == reviewId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Review>> GetByProviderAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Where(x => x.ProviderProfileId == providerProfileId)
            .OrderByDescending(x => x.CreatedOnUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsForServiceRequestAsync(
    Guid serviceRequestId,
    CancellationToken cancellationToken = default)
{
    return await _context.Reviews.AnyAsync(
        x => x.ServiceRequestId == serviceRequestId,
        cancellationToken);
}

    public void Update(Review review)
    {
        _context.Reviews.Update(review);
    }

    public void Remove(Review review)
    {
        _context.Reviews.Remove(review);
    }
}