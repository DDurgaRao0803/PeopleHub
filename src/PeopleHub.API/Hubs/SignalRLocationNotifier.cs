using Microsoft.AspNetCore.SignalR;
using PeopleHub.Application.Common.Interfaces.Realtime;

namespace PeopleHub.API.Hubs;

public sealed class SignalRLocationNotifier
    : IRealtimeNotifier
{
    private readonly IHubContext<LocationHub> _hubContext;


    public SignalRLocationNotifier(
        IHubContext<LocationHub> hubContext)
    {
        _hubContext = hubContext;
    }



    public async Task SendLocationUpdateAsync(
        Guid providerProfileId,
        decimal latitude,
        decimal longitude,
        DateTime updatedOnUtc,
        CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients
            .Group(providerProfileId.ToString())
            .SendAsync(
                "LocationUpdated",
                new
                {
                    providerProfileId,
                    latitude,
                    longitude,
                    updatedOnUtc
                },
                cancellationToken);
    }
}