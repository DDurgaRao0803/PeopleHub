using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers.Search;
using PeopleHub.Contracts.Common;
using PeopleHub.Contracts.Providers.Search;

namespace PeopleHub.Infrastructure.Providers.Search;

public sealed class ProviderSearchService : IProviderSearchService
{
    private readonly IProviderSearchRepository _providerSearchRepository;

    public ProviderSearchService(
        IProviderSearchRepository providerSearchRepository)
    {
        _providerSearchRepository = providerSearchRepository;
    }

    public Task<PagedResponse<ProviderSearchResponse>> SearchAsync(
        SearchProvidersRequest request,
        CancellationToken cancellationToken = default)
    {
        return _providerSearchRepository.SearchAsync(
            request,
            cancellationToken);
    }
}