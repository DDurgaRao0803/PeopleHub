using PeopleHub.Contracts.Common;
using PeopleHub.Contracts.Providers.Search;

namespace PeopleHub.Application.Providers.Search;

public interface IProviderSearchService
{
    Task<PagedResponse<ProviderSearchResponse>> SearchAsync(
        SearchProvidersRequest request,
        CancellationToken cancellationToken = default);
}