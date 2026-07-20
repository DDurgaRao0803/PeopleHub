using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.ServiceRequests;
using PeopleHub.Contracts.Providers.ServiceRequests;

namespace PeopleHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestsController : ControllerBase
{
    private readonly IServiceRequestService _serviceRequestService;

    public ServiceRequestsController(
        IServiceRequestService serviceRequestService)
    {
        _serviceRequestService = serviceRequestService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceRequestResponse>> Create(
        [FromQuery] Guid customerId,
        [FromBody] CreateServiceRequestRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _serviceRequestService.CreateAsync(
            customerId,
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceRequestResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var response = await _serviceRequestService.GetByIdAsync(
            id,
            cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ServiceRequestResponse>>> GetCustomerRequests(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        var response = await _serviceRequestService.GetCustomerRequestsAsync(
            customerId,
            cancellationToken);

        return Ok(response);
    }

    [HttpGet("provider/{providerProfileId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ServiceRequestResponse>>> GetProviderRequests(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        var response = await _serviceRequestService.GetProviderRequestsAsync(
            providerProfileId,
            cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id:guid}/accept")]
    public async Task<ActionResult<ServiceRequestResponse>> Accept(
        Guid id,
        CancellationToken cancellationToken)
    {
        var response = await _serviceRequestService.AcceptAsync(
            id,
            cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<ActionResult<ServiceRequestResponse>> Complete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var response = await _serviceRequestService.CompleteAsync(
            id,
            cancellationToken);

        return Ok(response);
    }
}