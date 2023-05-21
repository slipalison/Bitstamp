using Microsoft.AspNetCore.SignalR;

namespace HostedServicesClient.WebSockets;

public class WebSocketHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}