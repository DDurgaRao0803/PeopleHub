using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Contracts.Common;
using PeopleHub.Contracts.Providers.Search;
using PeopleHub.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class ProviderSearchRepository : IProviderSearchRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProviderSearchRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<ProviderSearchResponse>> SearchAsync(
    SearchProvidersRequest request,
    CancellationToken cancellationToken = default)
{
    var query =
        from profile in _dbContext.ProviderProfiles
        join user in _dbContext.Users
            on profile.UserId equals user.Id
        from skill in profile.Skills.DefaultIfEmpty()
        join category in _dbContext.ServiceCategories
            on skill.ServiceCategoryId equals category.Id into categories
        from category in categories.DefaultIfEmpty()
        select new
        {
            Profile = profile,
            User = user,
            CategoryName = category != null ? category.Name : string.Empty,
            CategoryId = category != null ? category.Id : (Guid?)null
        };

    if (!string.IsNullOrWhiteSpace(request.Keyword))
    {
        var keyword = request.Keyword.Trim().ToLower();

        query = query.Where(x =>
            (x.User.FirstName + " " + x.User.LastName)
                .ToLower()
                .Contains(keyword)
            || x.Profile.Bio
                .ToLower()
                .Contains(keyword));
    }

    if (request.ServiceCategoryId.HasValue)
    {
        query = query.Where(x =>
            x.CategoryId == request.ServiceCategoryId.Value);
    }

    if (request.VerifiedOnly)
    {
        query = query.Where(x =>
            x.Profile.VerificationStatus ==
            VerificationStatus.Approved);
    }

    var totalRecords = await query.CountAsync(cancellationToken);

    var items = await query
        .OrderBy(x => x.User.FirstName)
        .ThenBy(x => x.User.LastName)
        .Skip((request.Page - 1) * request.PageSize)
        .Take(request.PageSize)
        .Select(x => new ProviderSearchResponse
        {
            ProviderProfileId = x.Profile.Id,
            UserId = x.User.Id,
            FullName = $"{x.User.FirstName} {x.User.LastName}",
            Bio = x.Profile.Bio,
            ExperienceYears = x.Profile.ExperienceYears,
            ServiceCategory = x.CategoryName,
            IsVerified =
                x.Profile.VerificationStatus ==
                VerificationStatus.Approved
        })
        .ToListAsync(cancellationToken);

    return new PagedResponse<ProviderSearchResponse>(
        items,
        totalRecords,
        request.Page,
        request.PageSize);
}
}