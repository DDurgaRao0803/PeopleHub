using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.Search;
using PeopleHub.Contracts.Common;
using PeopleHub.Contracts.Providers.Search;

namespace PeopleHub.API.Controllers.Providers.Search;

[ApiController]
[Route("api/providers/search")]
public sealed class ProviderSearchController : ControllerBase
{
    private readonly IProviderSearchService _providerSearchService;

    public ProviderSearchController(
        IProviderSearchService providerSearchService)
    {
        _providerSearchService = providerSearchService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<ProviderSearchResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResponse<ProviderSearchResponse>>> Search(
        [FromQuery] SearchProvidersRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _providerSearchService.SearchAsync(
            request,
            cancellationToken);

        return Ok(result);
    }
}