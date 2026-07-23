using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers.Bidding;
using PeopleHub.Contracts.Providers.Bidding;

namespace PeopleHub.API.Controllers.Providers.Bidding;

[ApiController]
[Authorize]
[Route("api/service-requests/{serviceRequestId:guid}/bids")]
public sealed class ProviderBidsController : ControllerBase
{
    private readonly IProviderBidService _providerBidService;
    private readonly IProviderRepository _providerRepository;


    public ProviderBidsController(
        IProviderBidService providerBidService,
        IProviderRepository providerRepository)
    {
        _providerBidService = providerBidService;
        _providerRepository = providerRepository;
    }



    [HttpPost]
    public async Task<ActionResult<ProviderBidResponse>> SubmitBid(
        Guid serviceRequestId,
        [FromBody] CreateProviderBidRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var providerProfileId =
                await GetProviderProfileIdAsync(
                    cancellationToken);


            var bidRequest =
                request with
                {
                    ServiceRequestId = serviceRequestId
                };


            var response =
                await _providerBidService.SubmitBidAsync(
                    providerProfileId,
                    bidRequest,
                    cancellationToken);


            return CreatedAtAction(
                nameof(GetBids),
                new
                {
                    serviceRequestId
                },
                response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }



    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProviderBidResponse>>> GetBids(
        Guid serviceRequestId,
        CancellationToken cancellationToken)
    {
        var response =
            await _providerBidService.GetBidsAsync(
                serviceRequestId,
                cancellationToken);


        return Ok(response);
    }



    [HttpPost("{bidId:guid}/accept")]
    public async Task<ActionResult<ProviderBidResponse>> AcceptBid(
        Guid serviceRequestId,
        Guid bidId,
        CancellationToken cancellationToken)
    {
        try
        {
            var response =
                await _providerBidService.AcceptBidAsync(
                    bidId,
                    cancellationToken);


            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }



    private async Task<Guid> GetProviderProfileIdAsync(
        CancellationToken cancellationToken)
    {
        var userIdClaim =
            User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier);


        if (userIdClaim is null)
        {
            throw new UnauthorizedAccessException(
                "User identity not found.");
        }


        var userId =
            Guid.Parse(userIdClaim.Value);


        var provider =
            await _providerRepository.GetByUserIdAsync(
                userId,
                cancellationToken);


        if (provider is null)
        {
            throw new KeyNotFoundException(
                "Provider profile not found.");
        }


        return provider.Id;
    }
}