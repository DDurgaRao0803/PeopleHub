using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.Reviews;
using PeopleHub.Contracts.Providers.Reviews;

namespace PeopleHub.API.Controllers.Providers.Reviews;

[ApiController]
[Route("api/provider-reviews")]
public sealed class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        CreateReviewRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _reviewService.CreateAsync(
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetProviderReviews),
            new
            {
                providerProfileId = response.ProviderProfileId
            },
            response);
    }

    [HttpGet("{providerProfileId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<ReviewResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProviderReviews(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        var reviews = await _reviewService.GetProviderReviewsAsync(
            providerProfileId,
            cancellationToken);

        return Ok(reviews);
    }
}