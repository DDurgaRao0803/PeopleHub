using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Notifications;
using PeopleHub.Contracts.Notifications;

namespace PeopleHub.API.Controllers.Notifications;

[ApiController]
[Route("api/notifications")]
[Authorize]
public sealed class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;


    public NotificationsController(
        INotificationService notificationService)
    {
        _notificationService = notificationService;
    }



    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<NotificationResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<NotificationResponse>>> Get(
        CancellationToken cancellationToken)
    {
        var userId =
            GetUserId();


        var result =
            await _notificationService
                .GetUserNotificationsAsync(
                    userId,
                    cancellationToken);


        return Ok(result);
    }



    [HttpPost]
    [ProducesResponseType(
        typeof(NotificationResponse),
        StatusCodes.Status201Created)]
    public async Task<ActionResult<NotificationResponse>> Create(
        [FromBody] CreateNotificationRequest request,
        CancellationToken cancellationToken)
    {
        var userId =
            GetUserId();


        var result =
            await _notificationService
                .CreateAsync(
                    userId,
                    request,
                    cancellationToken);


        return CreatedAtAction(
            nameof(Get),
            result);
    }



    [HttpPut("{id:guid}/read")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> MarkRead(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _notificationService
            .MarkAsReadAsync(
                id,
                cancellationToken);


        return NoContent();
    }



    private Guid GetUserId()
    {
        var claim =
            User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier);


        if (claim is null)
        {
            throw new UnauthorizedAccessException(
                "User id missing.");
        }


        return Guid.Parse(claim.Value);
    }
}