using Microsoft.AspNetCore.SignalR;

namespace WebSocketsDemo.Services;

public class ChatHub : Hub 
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
