using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Application.Providers.Reviews;
using PeopleHub.Contracts.Providers.Reviews;

namespace PeopleHub.API.Controllers.Providers.Reviews;

[ApiController]
[Authorize]
[Route("api/provider-reviews")]
public sealed class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly ICurrentUserService _currentUserService;

    public ReviewsController(
        IReviewService reviewService,
        ICurrentUserService currentUserService)
    {
        _reviewService = reviewService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        CreateReviewRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _reviewService.CreateAsync(
            _currentUserService.UserId,
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