using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Application.Providers.ServiceRequests;
using PeopleHub.Contracts.Providers.ServiceRequests;

namespace PeopleHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/service-requests")]
public class ServiceRequestsController : ControllerBase
{
    private readonly IServiceRequestService _serviceRequestService;
    private readonly ICurrentUserService _currentUserService;

    public ServiceRequestsController(
        IServiceRequestService serviceRequestService,
        ICurrentUserService currentUserService)
    {
        _serviceRequestService = serviceRequestService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
public async Task<ActionResult<ServiceRequestResponse>> Create(
    [FromBody] CreateServiceRequestRequest request,
    CancellationToken cancellationToken)
{
    Console.WriteLine(">>> CREATE SERVICE REQUEST HIT");

    var response = await _serviceRequestService.CreateAsync(
        _currentUserService.UserId,
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
        Console.WriteLine(">>> GetCustomerRequests HIT");

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
        Console.WriteLine(">>> GetProviderRequests HIT");

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
    try
    {
        var response = await _serviceRequestService.AcceptAsync(
            id,
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

    [HttpPost("{serviceRequestId:guid}/reject")]
public async Task<ActionResult<ServiceRequestResponse>> Reject(
    Guid serviceRequestId,
    CancellationToken cancellationToken)
{
    try
    {
        var response = await _serviceRequestService.RejectAsync(
            serviceRequestId,
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

    [HttpPut("{id:guid}/complete")]
public async Task<ActionResult<ServiceRequestResponse>> Complete(
    Guid id,
    CancellationToken cancellationToken)
{
    try
    {
        var response = await _serviceRequestService.CompleteAsync(
            id,
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
}