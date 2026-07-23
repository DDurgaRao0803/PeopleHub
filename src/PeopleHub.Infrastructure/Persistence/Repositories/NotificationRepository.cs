using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Notifications;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class NotificationRepository
    : INotificationRepository
{
    private readonly ApplicationDbContext _context;


    public NotificationRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        await _context.Notifications.AddAsync(
            notification,
            cancellationToken);
    }



    public async Task<IReadOnlyList<Notification>> GetUserNotificationsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedOnUtc)
            .ToListAsync(cancellationToken);
    }



    public async Task<Notification?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }



    public Task UpdateAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        _context.Notifications.Update(notification);

        return Task.CompletedTask;
    }
}