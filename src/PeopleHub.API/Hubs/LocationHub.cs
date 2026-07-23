using Microsoft.AspNetCore.SignalR;

namespace PeopleHub.API.Hubs;

public sealed class LocationHub : Hub
{
    public async Task JoinProviderTracking(
        string providerProfileId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            providerProfileId);
    }



    public async Task LeaveProviderTracking(
        string providerProfileId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            providerProfileId);
    }
}