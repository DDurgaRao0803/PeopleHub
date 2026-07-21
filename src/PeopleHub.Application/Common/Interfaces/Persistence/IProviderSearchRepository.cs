using PeopleHub.Contracts.Common;
using PeopleHub.Contracts.Providers.Search;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IProviderSearchRepository
{
    Task<PagedResponse<ProviderSearchResponse>> SearchAsync(
        SearchProvidersRequest request,
        CancellationToken cancellationToken = default);
}